using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Static : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = ModContent.BuffType<ElectricShock>();
            dustType = DustID.Sand;
        }
    }
}