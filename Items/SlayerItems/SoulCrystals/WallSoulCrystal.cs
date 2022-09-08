using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class WallSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Summon 5 friendly The Hungry over the course of 5 seconds";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Wall of Flesh)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().WallSoul = true;
            }
            return base.UseItem(player);
        }
    }
}
