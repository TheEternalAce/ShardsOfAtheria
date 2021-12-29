using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles
{
    public class PacBlasterShot : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.arrow = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.light = .5f;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
        }
    }
}
