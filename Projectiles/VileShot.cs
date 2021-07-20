using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class VileShot : ModProjectile {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults() {
            projectile.width = 2;
            projectile.height = 2;
            projectile.magic = true;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            drawOffsetX = -19;
            drawOriginOffsetX = 9;
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
                drawOffsetX = -19;
                drawOriginOffsetX = 9;
            }
            else
            {
                drawOffsetX = 0;
                drawOriginOffsetX = -9; // Math works out that this is negative of the other value.
            }
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, DustID.Vile,
                    projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, DustID.Vile,
                    0, 0, 254, Scale: 0.3f);
                dust.velocity += projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
        }
    }
}
