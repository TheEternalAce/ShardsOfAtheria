using ShardsOfAtheria.Utilities;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Vile : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(8);
            Projectile.AddElement(0);
            Projectile.AddElement(3);
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