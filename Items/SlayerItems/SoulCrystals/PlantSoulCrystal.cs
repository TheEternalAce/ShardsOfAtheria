using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class PlantSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Spawn up to 8 tentacles over the course of 40 seconds\n" +
                "Attacks fire a petal that inflicts venom\n" +
                "Passive 15% increase in movement speed, 10% damage increase and increased life regen";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Plantera)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().PlantSoul = true;
            }
            return base.UseItem(player);
        }
    }
}
