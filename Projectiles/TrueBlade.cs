using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles
{
    public class TrueBlade : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.arrow = false;
            Projectile.light = 0.5f;
        }
    }

}
