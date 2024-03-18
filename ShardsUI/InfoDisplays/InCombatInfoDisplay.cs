using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ShardsUI.InfoDisplays
{
    // This example show how to create new informational display (like Radar, Watches, etc.)
    // Take a look at the ExampleInfoDisplayPlayer at the end of the file to see how to use it
    class InCombatInfoDisplay : InfoDisplay
    {
        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs(Main.LocalPlayer.Shards().combatTimer);

        // This dictates whether or not this info display should be active
        public override bool Active()
        {
            return Main.LocalPlayer.Shards().InCombat;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            // This is the value that will show up when viewing this display in normal play, right next to the icon
            return "In Combat";
        }
    }
}
