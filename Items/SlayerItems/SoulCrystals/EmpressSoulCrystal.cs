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
            Tooltip.SetDefault("");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().EmpressSoul = SoulCrystalStatus.Absorbed;
            }
            return base.UseItem(player);
        }
    }
}
