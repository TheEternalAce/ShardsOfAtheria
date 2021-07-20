using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SagesMania.Projectiles
{
    public class SolarFlare : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;

            projectile.aiStyle = 33;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.light = 1;
            projectile.penetrate = 10;
            aiType = ProjectileID.Flare;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(BuffID.Ichor, 600);
        }

        public override void AI()
        {
        }
    }
}
