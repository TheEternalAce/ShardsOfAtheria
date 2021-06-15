using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;

namespace SagesMania.Projectiles
{
    public class InfectionBulletProjectile : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infection Bullet");
        }

        public override void SetDefaults() {
            projectile.width = 2;
            projectile.height = 20;

            projectile.ranged = true;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(ModContent.BuffType<Infection>(), 10 * 60);
        }
    }
}
