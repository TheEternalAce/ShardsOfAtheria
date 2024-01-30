using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.InfoDisplays
{
    // This example show how to create new informational display (like Radar, Watches, etc.)
    // Take a look at the ExampleInfoDisplayPlayer at the end of the file to see how to use it
    class OverdriveTimeInfoDisplay : InfoDisplay
    {
        // This dictates whether or not this info display should be active
        public override bool Active()
        {
            return Main.LocalPlayer.Shards().Biometal;
        }

        // Here we can change the value that will be displayed in the game
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            // This is the value that will show up when viewing this display in normal play, right next to the icon
            return $"{Main.LocalPlayer.Shards().overdriveTimeCurrent} / {Main.LocalPlayer.Shards().overdriveTimeMax2}";
        }
    }
}
