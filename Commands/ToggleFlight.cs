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
			if (ModContent.GetInstance<SMWorld>().flightToggle)
			{
				ModContent.GetInstance<SMWorld>().flightToggle = false;
				Main.NewText("Flight disabled");
			}
			else
			{
				ModContent.GetInstance<SMWorld>().flightToggle = true;
				Main.NewText("Flight enabled");
			}
		}
    }
}
