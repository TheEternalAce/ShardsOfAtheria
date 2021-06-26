using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class SapphireBolt : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 32;
            projectile.height = 32;

            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            projectile.light = 1;
            aiType = ProjectileID.Bullet;
            drawOffsetX = 10;
        }
    }
}
