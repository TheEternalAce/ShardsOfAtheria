using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.InfoDisplays
{
    // This example show how to create new informational display (like Radar, Watches, etc.)
    // Take a look at the ExampleInfoDisplayPlayer at the end of the file to see how to use it
    class InCombatInfoDisplay : InfoDisplay
    {
        public override void SetStaticDefaults()
        {
            // This is the name that will show up when hovering over icon of this info display
            InfoName.SetDefault("In Combat");
        }

        // This dictates whether or not this info display should be active
        public override bool Active()
        {
            return Main.LocalPlayer.ShardsOfAtheria().inCombat;
        }

        // Here we can change the value that will be displayed in the game
        public override string DisplayValue()
        {
            // This is the value that will show up when viewing this display in normal play, right next to the icon
            return "In Combat";
        }
    }
}
