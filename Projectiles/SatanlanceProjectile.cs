using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class SatanlanceProjectile : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ecnalnataS");
        }

        public override void SetDefaults() {
            projectile.width = 80;
            projectile.height = 80;
            projectile.aiStyle = 19;
            projectile.penetrate = -1;
            projectile.scale = 1.3f;
            projectile.alpha = 0;

            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;

            aiType = ProjectileID.Spear;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Confused, 10*60);
            target.AddBuff(BuffID.CursedInferno, 10 * 60);
            target.AddBuff(BuffID.Frostburn, 10 * 60);
            target.AddBuff(BuffID.Ichor, 10 * 60);
        }
    }
}
