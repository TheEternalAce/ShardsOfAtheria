using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class ValkyrieSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Grants 8 defense, wing flight time boost and a dash that leaves behind an electric trail\n" +
                "Attacks create 4 closing feather blades in an x pattern\n" +
                "Getting hit by an enemy gives them Electric Shock";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Nova Stellar)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().ValkyrieSoul = true;
            }
            return base.UseItem(player);
        }
    }
}
