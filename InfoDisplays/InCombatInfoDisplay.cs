using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.InfoDisplays
{
    // This example show how to create new informational display (like Radar, Watches, etc.)
    // Take a look at the ExampleInfoDisplayPlayer at the end of the file to see how to use it
    class InCombatInfoDisplay : InfoDisplay
    {
        // This dictates whether or not this info display should be active
        public override bool Active()
        {
            return Main.LocalPlayer.Shards().InCombat;
        }

        // Here we can change the value that will be displayed in the game
        public override string DisplayValue(ref Color displayColor)
        {
            // This is the value that will show up when viewing this display in normal play, right next to the icon
            return "In Combat";
        }
    }
}
