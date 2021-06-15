using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;

namespace SagesMania.Projectiles
{
    public class InfectionBlob : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 32;
            projectile.height = 32;

            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(ModContent.BuffType<Infection>(), 10*60);
        }
    }

}
