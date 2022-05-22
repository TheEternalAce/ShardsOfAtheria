using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class EmpressSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Empress of Light)");
            Tooltip.SetDefault("Increased max flight time and permanent Shine and Night Owl buffs\n" +
                "Daytime increased damage by 20% and nighttime increases defense by 20\n" +
                "Hitting enemies summons a twilight lance for a second strike");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().EmpressSoul = true;
            }
            return base.UseItem(player);
        }
    }
}
