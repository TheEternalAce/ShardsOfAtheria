using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class DeerclopsSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "For every nearby NPC your damage is increased by 5% and your defense is increased by 10\n" +
                "This increase caps at 15% increased damage and 15 defense\n" +
                "Summons shadow hands on taking damage";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Deerclops)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
