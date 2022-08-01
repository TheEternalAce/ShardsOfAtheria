using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Potions;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class AreusArrow : ModProjectile
    {
        public static Asset<Texture2D> glowmask;

        public override void Load()
        {
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;

            DrawOffsetX = -4;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = 1;
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.timeLeft = 60;
            }
            if (Projectile.ai[0] == 2)
            {
                Projectile.timeLeft = 30;
                Projectile.ai[0] = 3;
            }
            if (Projectile.timeLeft == 1 && Projectile.ai[0] == 3)
            {
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                Projectile.timeLeft = 60;
                Projectile.ai[0] = 4;
            }
            if (Projectile.ai[0] == 4)
            {
                Projectile.penetrate = 1;
                Projectile.velocity *= -1;
                Projectile.ai[0] = 5;
            }
            if (Projectile.ai[0] == 5)
            {
                float maxDetectRadius = 400f; // The maximum radius at which a projectile can detect a target

                // Trying to find NPC closest to the projectile
                NPC closestNPC = FindClosestNPC(maxDetectRadius);
                if (closestNPC == null)
                    return;

                // If found, change the velocity of the projectile and turn it in the direction of the target
                // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
                Projectile.velocity =  (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 16;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
            if (Projectile.ai[0] == 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize((Projectile.position + Projectile.velocity) - Projectile.Center).RotatedBy(MathHelper.ToRadians(60 * i)) * 16,
                        ModContent.ProjectileType<AreusArrow>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 2f);
                }
            }
        }

        // Finding the closest NPC to attack within maxDetectDistance range
        // If not found then returns null
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public override void PostDraw(Color lightColor)
        {
            //TODO Generic glowmask draw, maybe generalize method
            Player player = Main.player[Projectile.owner];

            int offsetY = 0;
            int offsetX = 0;
            Texture2D glowmaskTexture = glowmask.Value;
            float originX = (glowmaskTexture.Width - Projectile.width) * 0.5f + Projectile.width * 0.5f;
            ProjectileLoader.DrawOffset(Projectile, ref offsetX, ref offsetY, ref originX);

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (Projectile.ownerHitCheck && player.gravDir == -1f)
            {
                if (player.direction == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                else if (player.direction == -1)
                {
                    spriteEffects = SpriteEffects.None;
                }
            }

            Vector2 drawPos = new Vector2(Projectile.position.X - Main.screenPosition.X + originX + offsetX, Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + Projectile.gfxOffY);
            Rectangle sourceRect = glowmaskTexture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Color glowColor = new Color(255, 255, 255, 255) * 0.7f * Projectile.Opacity;
            Vector2 drawOrigin = new Vector2(originX, Projectile.height / 2 + offsetY);
            Main.EntitySpriteDraw(glowmaskTexture, drawPos, sourceRect, glowColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
        }
    }
}
