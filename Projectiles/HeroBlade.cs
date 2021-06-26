using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class HeroBlade : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 6;
            projectile.height = 6;

            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = false;
            projectile.light = 0.5f;
        }
    }

}
