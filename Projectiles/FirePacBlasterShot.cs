using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class FirePacBlasterShot : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 14;
            projectile.height = 14;

            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.arrow = false;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.light = .5f;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }
    }
}
