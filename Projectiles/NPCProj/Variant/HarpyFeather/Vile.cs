using ShardsOfAtheria.Utilities;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Vile : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElementFire();
            Projectile.AddElementWood();
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Weak;
            dustType = DustID.VilePowder;
        }
    }
}