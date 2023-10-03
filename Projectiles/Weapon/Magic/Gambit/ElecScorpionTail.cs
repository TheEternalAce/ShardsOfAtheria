using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic.Gambit
{
    public class ElecScorpionTail : ModProjectile
    {
        // The folder path to the flail chain sprite
        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Weapon/Magic/Gambit/ElecScorpionTail_Chain";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.NoMeleeSpeedVelocityScaling[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.penetrate = -1; // Make the flail infinitely penetrate like other flails
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 60;
        }

        // This AI code is adapted from the aiStyle 15. We need to re-implement this to customize the behavior of our flail
        public override void AI()
        {
            var player = Main.player[Projectile.owner];

            // If owner player dies, remove the flail.
            if (player.dead)
            {
                Projectile.Kill();
                return;
            }

            // This prevents the item from being able to be used again prior to this projectile dying
            player.itemAnimation = 10;
            player.itemTime = 10;

            // Here we turn the player and projectile based on the relative positioning of the player and Projectile.
            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.spriteDirection = Projectile.direction = newDirection;

            if (Main.rand.NextBool(5))
            {
                Vector2 vector = Projectile.velocity;
                vector.Normalize();
                vector *= 4f;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Electric, vector.X, vector.Y);
            }

            // Here we set the rotation based off of the direction to the player tweaked by the velocity, giving it a little spin as the flail turns around each swing 
            Projectile.rotation = Vector2.Normalize(Projectile.Center - player.Center).ToRotation() + MathHelper.ToRadians(90);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // if we should play the sound..
            Projectile.netUpdate = true;
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            // Play the sound
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);

            return base.OnTileCollide(oldVelocity);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff<ElectricShock>(600);
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Poisoned, 600);
            }
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
                drawPosition += remainingVectorToPlayer * 24 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}
