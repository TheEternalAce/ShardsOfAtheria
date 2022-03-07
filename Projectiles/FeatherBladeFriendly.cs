using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Projectiles
{
    public class FeatherBladeFriendly : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/FeatherBlade";

        public override void SetDefaults() {
            Projectile.CloneDefaults(ModContent.ProjectileType<FeatherBlade>());
			Projectile.friendly = true;
			Projectile.hostile = false;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }
    }
}
