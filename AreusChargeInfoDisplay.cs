using ShardsOfAtheria.Items.Weapons;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
	// This example show how to create new informational display (like Radar, Watches, etc.)
	// Take a look at the ExampleInfoDisplayPlayer at the end of the file to see how to use it
	class AreusChargeInfoDisplay : InfoDisplay
	{
		public override void SetStaticDefaults()
		{
			// This is the name that will show up when hovering over icon of this info display
			InfoName.SetDefault("Areus Charge");
		}

		// This dictates whether or not this info display should be active
		public override bool Active()
		{
			return Main.LocalPlayer.HeldItem.ModItem is AreusWeapon;
		}

		// Here we can change the value that will be displayed in the game
		public override string DisplayValue()
		{
			// This is the value that will show up when viewing this display in normal play, right next to the icon
			return $"Charge: {(Main.LocalPlayer.HeldItem.ModItem as AreusWeapon).areusCharge}%";
		}
	}
}