using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Commands
{
    class ToggleGravitation : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "toggleGrav";

		public override string Description
			=> "Toggle Mega Gem Core Gravitation buff";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			Player player = Main.LocalPlayer;
			if (player.GetModPlayer<SMPlayer>().megaGemCoreGrav)
			{
				player.GetModPlayer<SMPlayer>().megaGemCoreGrav = false;
				Main.NewText("Mega Gem Core Gravitation disabled");
			}
			else
			{
				player.GetModPlayer<SMPlayer>().megaGemCoreGrav = true;
				Main.NewText("Mega Gem Core Gravitation enabled");
			}
		}
    }
}
