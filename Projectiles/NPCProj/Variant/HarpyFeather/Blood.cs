using ShardsOfAtheria.Utilities;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Blood : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Ichor;
            dustType = DustID.Blood;
        }
    }
}