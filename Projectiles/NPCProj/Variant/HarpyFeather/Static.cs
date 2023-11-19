using ShardsOfAtheria.Buffs.AnyDebuff;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Static : HarpyFeathers
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = ModContent.BuffType<ElectricShock>();
            dustType = DustID.Sand;
        }
    }
}