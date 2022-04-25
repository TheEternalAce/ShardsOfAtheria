using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class SolarFlare : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.aiStyle = 33;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.light = 1;
            Projectile.penetrate = 10;
            AIType = ProjectileID.Flare;
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
