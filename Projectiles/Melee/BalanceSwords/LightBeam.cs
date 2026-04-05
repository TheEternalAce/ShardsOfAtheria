using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.BalanceSwords
{
    public class LightBeam : HitscanBullet_Electric
    {
        public override void StaticProperties()
        {
            Projectile.AddDamageType(10);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(8);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Melee;
            arcColor = Color.Gold;
            thickness = 4;
        }
    }
}
