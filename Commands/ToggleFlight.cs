using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
    class ToggleFlight : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "toggleFlight";

		public override string Description
			=> "Toggle flight";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			Player player = Main.LocalPlayer;
			if (!ModContent.GetInstance<SMWorld>().flightDisabled)
			{
				ModContent.GetInstance<SMWorld>().flightDisabled = true;
				Main.NewText("Flight disabled");
			}
			else
			{
				ModContent.GetInstance<SMWorld>().flightDisabled = false;
				Main.NewText("Flight enabled");
			}
		}
    }
}
