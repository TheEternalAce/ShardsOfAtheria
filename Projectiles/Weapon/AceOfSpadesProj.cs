using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Projectiles.Weapon
{
    public class AceOfSpadesProj : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 30;
            Projectile.height = 40;
            Projectile.scale = .5f;

            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            AIType = ProjectileID.Bullet;
        }
    }
}
