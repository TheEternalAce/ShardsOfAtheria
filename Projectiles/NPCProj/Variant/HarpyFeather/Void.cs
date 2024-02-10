using ShardsOfAtheria.Utilities;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Void : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.OnFire;
            dustType = DustID.Torch;
        }
    }
}