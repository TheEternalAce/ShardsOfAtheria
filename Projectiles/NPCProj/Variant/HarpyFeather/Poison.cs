using ShardsOfAtheria.Utilities;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Poison : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(10);
            Projectile.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Poisoned;
            dustType = DustID.Poisoned;
        }
    }
}