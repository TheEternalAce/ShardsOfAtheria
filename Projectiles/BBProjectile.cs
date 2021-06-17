using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class BBProjectile : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BB");
        }

        public override void SetDefaults() {
            projectile.width = 2;
            projectile.height = 20;
            projectile.damage = 4;
            projectile.ranged = true;

            projectile.ranged = true;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }
    }
}
