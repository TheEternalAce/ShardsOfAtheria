using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Commands
{
    class ToggleFlight : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "toggleFlight";

		public override string Description
			=> "Allow or dissallow flight";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			Player player = Main.LocalPlayer;
			if (player.GetModPlayer<SMPlayer>().flightToggle == 0)
			{
				player.GetModPlayer<SMPlayer>().flightToggle = 1;
				Main.NewText("Flight disabled");
			}
			else
			{
				player.GetModPlayer<SMPlayer>().flightToggle = 0;
				Main.NewText("Flight enabled");
			}
		}
    }
}
