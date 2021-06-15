using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class PhantomBullet : ModProjectile {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults() {
            projectile.width = 2;
            projectile.height = 20;

            projectile.ranged = true;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.arrow = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }
    }
}
