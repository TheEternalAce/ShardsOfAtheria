using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class VileShot : ModProjectile {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults() {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            DrawOffsetX = -19;
            DrawOriginOffsetX = 9;
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
                DrawOffsetX = -19;
                DrawOriginOffsetX = 9;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -9; // Math works out that this is negative of the other value.
            }
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Demonite,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Demonite,
                    0, 0, 254, Scale: 0.3f);
                dust.velocity += Projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
        }
    }
}
