using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class SkullSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "While in combat enter a \"spin phase\" for 5 seconds every 30 seconds\n" +
                "This \"spin phase\" inceases defense and damage by 50% and damages nearby enemies\n" +
                "Attacks fire a homing skull";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Skeletron)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
