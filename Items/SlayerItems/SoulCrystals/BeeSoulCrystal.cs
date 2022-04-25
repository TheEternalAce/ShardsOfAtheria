using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class BeeSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Queen Bee)");
            Tooltip.SetDefault("Passive Hive Pack and Honey Comb effects as well as 5% increased movement speed");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().BeeSoul = SoulCrystalStatus.Absorbed;
            }
            return base.UseItem(player);
        }
    }
}
