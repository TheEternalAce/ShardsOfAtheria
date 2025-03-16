using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class StrikeChain : ModProjectile
    {
        public const int FadeInDuration = 7;
        public const int FadeOutDuration = 4;

        public const int TotalDuration = 40;

        // The "width" of the blade
        public float CollisionWidth => 10f * Projectile.scale;

        public int Timer;
        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Melee/StrikeChain_Chain";

        public override void SetStaticDefaults()
        {
            Projectile.MakeTrueMelee();
            Projectile.AddDamageType(5);
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 5;
            Projectile.light = 0.5f;

            DrawOffsetX = -3;
        }

        public override void AI()
        {
            if (IsStickingToTarget)
            {
                StickAI();
            }
            else
            {
                NormalAI();
            }
        }

        private void NormalAI()
        {
            Player player = Main.player[Projectile.owner];

            Timer++;
            if (Timer >= TotalDuration)
            {
                // Kill the projectile if it reaches it's intented lifetime
                Projectile.Kill();
                return;
            }
            else
            {
                // Important so that the sprite draws "in" the player's hand and not fully infront or behind the player
                player.heldProj = Projectile.whoAmI;
            }

            // Fade in and out
            // GetLerpValue returns a value between 0f and 1f - if clamped is true - representing how far Timer got along the "distance" defined by the first two parameters
            // The first call handles the fade in, the second one the fade out.
            // Notice the second call's parameters are swapped, this means the result will be reverted
            Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

            // Keep locked onto the player, but extend further based on the given velocity (Requires ShouldUpdatePosition returning false to work)
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Projectile.velocity.Normalize();
            Projectile.velocity *= 26;
            if (Timer >= 20)
            {
                float reverseTimer = (20 * -1 + (Timer - 20)) * -1;
                Projectile.Center = playerCenter + Projectile.velocity * (reverseTimer + 1f);
            }
            else
            {
                Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);
            }
            if (Timer == 20)
            {
                float numberProjectiles = 3;
                float rotation = MathHelper.PiOver2;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    perturbedSpeed.Normalize();
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                        perturbedSpeed * 1f, ModContent.ProjectileType<LightningBoltFriendly>(),
                        Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            }
        }

        // Are we sticking to a target?
        public bool IsStickingToTarget
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        // Index of the current target
        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        private void StickAI()
        {
            // These 2 could probably be moved to the ModifyNPCHit hook, but in vanilla they are present in the AI
            Projectile.ignoreWater = true; // Make sure the projectile ignores water
            Projectile.tileCollide = false; // Make sure the projectile doesn't collide with tiles anymore
            const int aiFactor = 15; // Change this factor to change the 'lifetime' of this sticking javelin
            Projectile.localAI[0] += 1f;

            bool hitEffect = Projectile.localAI[0] % 30f == 0f;
            int projTargetIndex = TargetWhoAmI;
            if (Projectile.localAI[0] >= 60 * aiFactor || projTargetIndex < 0 || projTargetIndex >= 200)
            {
                Projectile.Kill();
            }
            else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage)
            {
                Projectile.Center = Main.npc[projTargetIndex].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;

                var player = Main.player[Projectile.owner];

                Vector2 vector2 = Projectile.Center - player.Center;
                Projectile.rotation = vector2.ToRotation() + MathHelper.ToRadians(90f);

                if (hitEffect)
                {
                    var stucknpc = Main.npc[projTargetIndex];
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center,
                        Vector2.Normalize(stucknpc.Center - player.Center),
                        ModContent.ProjectileType<StrikeChainCurrent>(), 160,
                        Projectile.knockBack, Projectile.owner, stucknpc.whoAmI);
                }
            }
            else
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
            var player = Main.player[Projectile.owner];
            if (player.Shards().Overdrive)
            {
                IsStickingToTarget = true; // we are sticking to a target
                TargetWhoAmI = target.whoAmI; // Set the target whoAmI
                Projectile.velocity =
                    (target.Center - Projectile.Center) *
                    0.75f; // Change velocity based on delta center of targets (difference between entity centers)
                Projectile.netUpdate = true; // netUpdate this javelin
                Projectile.timeLeft = 3600;
                Timer = 0;
                Projectile.friendly = false; // Makes sure the sticking javelins do not deal damage anymore
            }
        }

        public override bool ShouldUpdatePosition()
        {
            return IsStickingToTarget;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);

            var drawPosition = Projectile.Center;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            if (Projectile.alpha == 0)
            {
                int direction = -1;

                if (Projectile.Center.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (true)
            {
                float length = remainingVectorToPlayer.Length();

                // Once the remaining length is small enough, we terminate the loop
                if (length < 25f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToPlayer * 18 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}
