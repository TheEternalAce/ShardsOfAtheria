using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Commands
{
    class Megamerge : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "Megamerge!";

		public override string Description
			=> "Toggle visual effect of Living Metal";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			Player player = Main.LocalPlayer;
			if (player.GetModPlayer<SMPlayer>().megamergeToggle == 0)
			{
				player.GetModPlayer<SMPlayer>().megamergeToggle = 1;
				CombatText.NewText(player.Hitbox, Color.White, "MEGAMERGE!");
			}
			else
			{
				player.GetModPlayer<SMPlayer>().megamergeToggle = 0;
			}
		}
    }
}
