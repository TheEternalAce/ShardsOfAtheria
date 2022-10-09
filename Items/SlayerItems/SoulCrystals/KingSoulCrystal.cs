using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class KingSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Increased life and mana regen\n" +
                "After taking damage, your next hit will heal 25% of that damage taken";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (King Slime)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
