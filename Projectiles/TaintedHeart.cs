using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class TaintedHeart : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 32;
            projectile.height = 32;

            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Lovestruck, 600);
            target.AddBuff(BuffID.CursedInferno, 600);
            target.AddBuff(BuffID.Ichor, 600);
        }
    }
}
