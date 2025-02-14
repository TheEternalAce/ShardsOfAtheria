using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.WandAreus
{
    public class ElectricHook : ModProjectile
    {
        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Magic/WandAreus/ElectricHook_Chain";

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 60;
            Projectile.extraUpdates = 3;

            DrawOffsetX = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector2 = Projectile.Center - player.Center;

            player.ChangeDir(Projectile.Center.X > player.Center.X ? 1 : -1);
            player.itemAnimation = 2;
            player.itemTime = 2;
            player.itemRotation = Projectile.rotation = vector2.ToRotation() + MathHelper.ToRadians(90f);


            if (IsStickingToTarget)
            {
                StickAI(player);
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

        private void StickAI(Player player)
        {
            // These 2 could probably be moved to the ModifyNPCHit hook, but in vanilla they are present in the AI
            Projectile.ignoreWater = true; // Make sure the projectile ignores water
            Projectile.tileCollide = false; // Make sure the projectile doesn't collide with tiles anymore
            const int aiFactor = 15; // Change this factor to change the 'lifetime' of this sticking javelin
            //Projectile.timeLeft = 10;

            int projTargetIndex = TargetWhoAmI;
            var target = Main.npc[projTargetIndex];
            int targetSize = (target.width + target.height) / 4;

            if (Projectile.localAI[0] >= 60 * aiFactor || projTargetIndex < 0 || projTargetIndex >= 200 || Projectile.Distance(player.Center) < 50 + targetSize)
            {
                if (target.lifeMax < 2000 && !target.boss && target.CanBeChasedBy())
                {
                    target.velocity *= 0;
                }
                else
                {
                    player.velocity *= 0;
                }
                player.itemAnimation = 0;
                Projectile.Kill();
            }
            else if (target.active && !target.dontTakeDamage)
            {
                Projectile.Center = target.Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = target.gfxOffY;

                Vector2 vector2 = Projectile.Center - player.Center;
                vector2.Normalize();
                vector2 *= 36;
                if (target.lifeMax < 2000 && !target.boss && target.CanBeChasedBy())
                {
                    target.velocity = -vector2;
                }
                else
                {
                    player.velocity = vector2;
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
            Projectile.GetPlayerOwner().AddBuff<WandBuff>(2);
            IsStickingToTarget = true; // we are sticking to a target
            TargetWhoAmI = target.whoAmI; // Set the target whoAmI
            Projectile.velocity =
                (target.Center - Projectile.Center) *
                0.75f; // Change velocity based on delta center of targets (difference between entity centers)
            Projectile.netUpdate = true; // netUpdate this javelin
            Projectile.timeLeft = 600;
            Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore
        }

        public override bool ShouldUpdatePosition()
        {
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            lightColor = SoA.ElectricColorA;
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
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, lightColor, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
