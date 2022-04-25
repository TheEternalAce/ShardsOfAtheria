using ShardsOfAtheria.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class ElectricBlade : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.scale = 1.5f;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.arrow = false;
            Projectile.light = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 10*60);
        }

        public override void AI()
        {
            Projectile.rotation += 0.4f * (float)Projectile.direction;
            if (Main.rand.NextBool(20))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, Projectile.velocity.X* .2f, Projectile.velocity.Y* .2f, 200, Scale: 1f);
            }
        }
    }
}
