using ShardsOfAtheria.Players;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
	class GenericCommand : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "generic";
		// /generic

		public override string Description
			=> "Used by the mod developers to help with certain features";

		//create a break point on the bellow line, run the command, then make the effect
		//Reset before debugging
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			Player player = Main.LocalPlayer;
			SlayerPlayer slayer = Main.LocalPlayer.Slayer();
			ShardsPlayer soaPlayer = Main.LocalPlayer.ShardsOfAtheria();
			OverchargePlayer overchargePlayer = Main.LocalPlayer.Overcharged();
			ShardsDownedSystem soaWorld = ModContent.GetInstance<ShardsDownedSystem>();

			Console.WriteLine("----------Hello mod developer----------");
			soaPlayer.genesisRagnarockUpgrades = 0;
			ShardsHelpers.Log("Reset Genesis and Ragnarok upgrades", true);
		}
	}
}
