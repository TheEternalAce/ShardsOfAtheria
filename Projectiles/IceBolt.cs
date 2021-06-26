using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SagesMania.Projectiles
{
    public class IceBolt : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 18;
            projectile.height = 40;

            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            projectile.light = 1;
            projectile.magic = true;
            aiType = ProjectileID.Bullet;
            drawOffsetX = 10;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 10 * 60);
        }
    }

}
