using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class NotLime : ModProjectile {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.magic = true;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            drawOffsetX = -16;
            drawOriginOffsetX = 8;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            projectile.spriteDirection = projectile.direction = (projectile.velocity.X > 0).ToDirectionInt();
            // Adding Pi to rotation if facing left corrects the drawing
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
            if (projectile.spriteDirection == 1) // facing right
            {
                drawOffsetX = -16;
                drawOriginOffsetX = 8;
            }
            else
            {
                drawOffsetX = 0;
                drawOriginOffsetX = -8; // Math works out that this is negative of the other value.
            }
        }
    }
}
