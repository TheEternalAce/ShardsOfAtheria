using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class LordSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Moon Lord)");
            Tooltip.SetDefault("");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().LordSoul = SoulCrystalStatus.Absorbed;
            }
            return base.UseItem(player);
        }
    }
}
