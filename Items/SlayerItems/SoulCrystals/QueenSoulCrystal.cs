using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class QueenSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Queen Slime)");
            Tooltip.SetDefault("Increased life and mana regen\n" +
                "After taking damage, your next hit will heal 50% of that damage");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().QueenSoul = SoulCrystalStatus.Absorbed;
            }
            return base.UseItem(player);
        }
    }
}
