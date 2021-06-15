using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SagesMania.Projectiles
{
    public class ElectricBolt : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 32;
            projectile.height = 32;

            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            projectile.light = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Electrified, 600);
        }
    }

}
