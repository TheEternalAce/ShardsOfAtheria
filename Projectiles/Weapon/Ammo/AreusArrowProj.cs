using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements.Utilities;
using ReLogic.Content;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ammo
{
    public class AreusArrowProj : ModProjectile
    {
        public static Asset<Texture2D> glowmask;

        Point point;

        public override void Load()
        {
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.AreusProj.Add(Type);
            Projectile.AddElec();
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;

            DrawOffsetX = -4;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Projectile.netUpdate = true;

            if (Main.myPlayer == Projectile.owner)
            {
                Player player = Main.player[Projectile.owner];

                if (Projectile.ai[0] == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item91, Projectile.position);
                    point = (player.MountedCenter + Projectile.velocity + (Projectile.rotation - MathHelper.ToRadians(90)).ToRotationVector2().SafeNormalize(Vector2.Zero) * Vector2.Distance(player.Center, Main.MouseWorld)).ToPoint();
                    Projectile.tileCollide = true;
                    Projectile.velocity *= 0.9f;
                    Projectile.ai[0] = 1f;
                }
                if (Projectile.ai[0] == 1f)
                {
                    Dust dust = Dust.NewDustPerfect(point.ToVector2(), DustID.Electric, Vector2.Zero);
                    dust.noGravity = true;
                    if (Projectile.Hitbox.Contains(point))
                    {
                        Projectile.Kill();
                    }
                }
                if (Projectile.ai[0] == 2f)
                {
                    Projectile.timeLeft = 30;
                    Projectile.ai[0] = 3f;
                    Projectile.netUpdate = true;
                }
                if (Projectile.timeLeft == 1 && Projectile.ai[0] == 3f)
                {
                    SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                    Projectile.timeLeft = 60;
                    Projectile.penetrate = 1;
                    Projectile.velocity *= -1;
                    Projectile.ai[0] = 4f;
                }
                if (Projectile.ai[0] == 4f)
                {
                    float maxDetectRadius = 400f; // The maximum radius at which a projectile can detect a target

                    // Trying to find NPC closest to the projectile
                    NPC closestNPC = Projectile.FindClosestNPC(maxDetectRadius);
                    if (closestNPC == null)
                        return;

                    // If found, change the velocity of the projectile and turn it in the direction of the target
                    // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
                    Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 16;
                    Projectile.netUpdate = true;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.ai[0] == 1f)
            {
                Projectile.ai[0] = 5f;
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Player player = Main.player[Projectile.owner];
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                if (Projectile.ai[0] == 1f)
                {
                    int damage = Projectile.damage;
                    damage /= 3;
                    for (int i = 0; i < 6; i++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(60 * i)) * 16,
                            ModContent.ProjectileType<AreusArrowProj>(), damage, Projectile.knockBack, player.whoAmI, 2f);
                        Projectile.netUpdate = true;
                    }
                }
            }
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
