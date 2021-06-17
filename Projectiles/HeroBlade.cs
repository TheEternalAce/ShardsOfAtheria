using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class HeroBlade : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 31;
            projectile.height = 31;

            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = false;
            projectile.light = 0.5f;
        }
    }

}
