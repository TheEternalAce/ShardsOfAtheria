using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class BeeSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Attacks inflict Poisoned and shoot stingers\n" +
                "Spawn a bee every 10 seconds while in combat";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Queen Bee)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().BeeSoul = true;
            }
            return base.UseItem(player);
        }
    }
}
