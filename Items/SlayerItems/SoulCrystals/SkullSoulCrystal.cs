using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class SkullSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Skeletron)");
            Tooltip.SetDefault("While in combat enter a \"spin phase\" for 5 seconds every 30 seconds\n" +
                "This \"spin phase\" inceases defense and damage by 50% and damages nearby enemies\n" +
                "Attacks fire a homing skull");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().SkullSoul = SoulCrystalStatus.Absorbed;
            }
            return base.UseItem(player);
        }
    }
}
