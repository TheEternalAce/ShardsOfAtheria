using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.HeroSword
{
    public class HeroBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(3);
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.arrow = false;
            Projectile.light = 0.5f;
        }
    }
}
