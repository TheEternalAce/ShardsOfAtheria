using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
    class SlayAll : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "slayAll";

		public override string Description
			=> "Make all bosses slain";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			Player player = Main.LocalPlayer;
			if (player.name != "The Eternal Ace")
				return;

			ModContent.GetInstance<SoADownedSystem>().slainValkyrie = true;
			ModContent.GetInstance<SoADownedSystem>().slainEOC = true;
			ModContent.GetInstance<SoADownedSystem>().slainBOC = true;
			ModContent.GetInstance<SoADownedSystem>().slainEOW = true;
			ModContent.GetInstance<SoADownedSystem>().slainBee = true;
			ModContent.GetInstance<SoADownedSystem>().slainSkull = true;
			ModContent.GetInstance<SoADownedSystem>().slainWall = true;
			ModContent.GetInstance<SoADownedSystem>().slainMechWorm = true;
			ModContent.GetInstance<SoADownedSystem>().slainTwins = true;
			ModContent.GetInstance<SoADownedSystem>().slainPrime = true;
			ModContent.GetInstance<SoADownedSystem>().slainPlant = true;
			ModContent.GetInstance<SoADownedSystem>().slainGolem = true;
			ModContent.GetInstance<SoADownedSystem>().slainDuke = true;
			ModContent.GetInstance<SoADownedSystem>().slainEmpress = true;
			ModContent.GetInstance<SoADownedSystem>().slainMoonLord = true;
			ModContent.GetInstance<SoADownedSystem>().slainSenterra = true;
			ModContent.GetInstance<SoADownedSystem>().slainGenesis = true;
			ModContent.GetInstance<SoADownedSystem>().slainEverything = true;
			ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("All bosses are slain"), Color.White, player.whoAmI);
		}
    }
}
