using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles
{
    public class IonCutter : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 24;
            Projectile.height = 80;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.light = .5f;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 240;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            // Adding Pi to rotation if facing left corrects the drawing
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -16;
                DrawOriginOffsetX = 8;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -8; // Math works out that this is negative of the other value.
            }
        }
    }
}
