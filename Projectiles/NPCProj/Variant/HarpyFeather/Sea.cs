using ShardsOfAtheria.Utilities;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Sea : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(3);
            Projectile.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.ignoreWater = true;
            debuffType = BuffID.Venom;
            dustType = DustID.Water;
        }
    }
}