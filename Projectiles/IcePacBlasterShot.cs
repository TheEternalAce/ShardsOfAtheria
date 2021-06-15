using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class IcePacBlasterShot : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 14;
            projectile.height = 32;

            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.arrow = false;
            projectile.penetrate = 10;
            projectile.ignoreWater = true;
            projectile.light = 1;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Frostburn, 600);
        }
    }
}
