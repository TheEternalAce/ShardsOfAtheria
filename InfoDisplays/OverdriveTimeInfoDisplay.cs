﻿using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.InfoDisplays
{
    // This example show how to create new informational display (like Radar, Watches, etc.)
    // Take a look at the ExampleInfoDisplayPlayer at the end of the file to see how to use it
    class OverdriveTimeInfoDisplay : InfoDisplay
    {
        public override void SetStaticDefaults()
        {
            // This is the name that will show up when hovering over icon of this info display
            InfoName.SetDefault("Overdrive Time");
        }

        // This dictates whether or not this info display should be active
        public override bool Active()
        {
            return Main.LocalPlayer.HasBuff(ModContent.BuffType<Megamerged>());
        }

        // Here we can change the value that will be displayed in the game
        public override string DisplayValue()
        {
            // This is the value that will show up when viewing this display in normal play, right next to the icon
            return $"{Main.LocalPlayer.GetModPlayer<SoAPlayer>().overdriveTimeCurrent} / {Main.LocalPlayer.GetModPlayer<SoAPlayer>().overdriveTimeMax2}";
        }
    }
}
