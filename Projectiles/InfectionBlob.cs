using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Projectiles
{
    public class InfectionBlob : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.arrow = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Infection>(), 10*60);
        }
    }
}
