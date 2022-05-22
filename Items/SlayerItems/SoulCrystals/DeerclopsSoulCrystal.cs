using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class DeerclopsSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Deerclops)");
            Tooltip.SetDefault("For every nearby NPC your damage is increased by 5% and your defense is increased by 10\n" +
                "This increase caps at 15% increased damage and 15 defense\n" +
                "Summons shadow hands when you are hurt");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().DeerclopsSoul = true;
            }
            return base.UseItem(player);
        }
    }
}
