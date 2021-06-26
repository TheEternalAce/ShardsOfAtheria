using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SagesMania.Projectiles
{
    public class AceOfSpadesProj : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 30;
            projectile.height = 40;
            projectile.scale = .5f;

            projectile.aiStyle = 1;
            projectile.friendly = true;
            aiType = ProjectileID.Bullet;
        }
    }
}
