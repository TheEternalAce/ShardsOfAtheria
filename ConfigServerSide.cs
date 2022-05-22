using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria
{
    public class ConfigServerSide : ModConfig
    {
		// ConfigScope.ClientSide should be used for client side, usually visual or audio tweaks.
		// ConfigScope.ServerSide should be used for basically everything else, including disabling items or changing NPC behaviours
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Header("Mechanics")] // Headers are like titles in a Config. You only need to declare a header on the item it should appear over, not every item in the category.
		[Label("Areus weapons use mana")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
		[Tooltip("Disables the Areus Charge bar")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
		[DefaultValue(false)] // This sets the Configs default value.
		[ReloadRequired] // Marking it with [ReloadRequired] makes tModLoader force a mod reload if the option is changed. It should be used for things like item toggles, which only take effect during mod loading
		public bool areusWeaponsCostMana;
	}
}
