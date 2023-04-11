using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus.AreusSaber
{
    public class AreusSlash4 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.AreusProj.Add(Type);
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 36;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 240;

            DrawOffsetX = -2;
            DrawOriginOffsetY = -2;
            DrawOriginOffsetX = 0;
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
                DrawOffsetX = -2; // These values match the values in SetDefaults
                DrawOriginOffsetY = -2;
                DrawOriginOffsetX = 0;
            }
            else
            {
                // Facing left.
                // You can figure these values out if you flip the sprite in your drawing program.
                DrawOffsetX = 0; // 0 since now the top left corner of the hitbox is on the far left pixel.
                DrawOriginOffsetY = -2; // doesn't change
                DrawOriginOffsetX = 0; // Math works out that this is negative of the other value.
            }
        }
    }
}
