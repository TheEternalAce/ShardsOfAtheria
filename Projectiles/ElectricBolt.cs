using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Projectiles
{
    public class ElectricBolt : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.arrow = true;
            Projectile.light = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
        }

        public override void AI()
        {
            if (Main.rand.NextBool(20))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, Projectile.velocity.X* .2f, Projectile.velocity.Y* .2f, 200, Scale: 1f);
            }
        }
    }
}
