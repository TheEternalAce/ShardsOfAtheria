using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class GolemSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Grants the effects of Shiny Stone\n" +
                "While under 50% max life, gain increased life regen and summon a Golem head above you";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Golem)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
