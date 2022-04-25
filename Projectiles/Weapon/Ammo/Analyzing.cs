using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ammo
{
    public class Analyzing : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Analyzing");
        }

        public override void SetDefaults() {
            Projectile.width = 2;
            Projectile.height = 2;

            Projectile.aiStyle = -1;
            Projectile.timeLeft = 2;
        }
    }
}
