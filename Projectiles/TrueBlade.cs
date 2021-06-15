using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class TrueBlade : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 32;
            projectile.height = 32;

            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = false;
            projectile.light = 0.5f;
        }
    }

}
