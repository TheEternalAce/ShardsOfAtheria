using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ShockTome
{
    public class Vine : ModProjectile
    {
        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Magic/ShockTome/Vine_Chain";
        Vector2 spawnPoint;
        Vector2 initialVelocity;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.NoMeleeSpeedVelocityScaling[Type] = true;
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 90;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            spawnPoint = Projectile.Center;
            initialVelocity = Projectile.velocity;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.Center.DirectionFrom(spawnPoint).ToRotation() + MathHelper.PiOver2;
            if (Projectile.timeLeft > 60 && Main.rand.NextBool(5))
            {
                Vector2 vector = Projectile.velocity;
                vector.Normalize();
                vector *= 4f;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Grass, vector.X, vector.Y);
            }
            if (Projectile.timeLeft == 30) Projectile.velocity *= -1;
        }

        public override bool ShouldUpdatePosition()
        {
            return Projectile.timeLeft > 60 || Projectile.timeLeft <= 30;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return ShardsHelpers.DeathrayHitbox(spawnPoint, Projectile.Center, targetHitbox, 6f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            //lightColor = SoA.ElectricColorA;
            Vector2 mountedCenter = spawnPoint - initialVelocity;
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
                drawPosition += remainingVectorToPlayer * 16 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, lightColor, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}
