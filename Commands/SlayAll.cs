using Microsoft.Xna.Framework;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.Chat;
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

			ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainEOC = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainBOC = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainEOW = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainBee = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainSkull = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainWall = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainMechWorm = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainTwins = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainPrime = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainPlant = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainGolem = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainDuke = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainEmpress = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainMoonLord = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainSenterra = true;
			ModContent.GetInstance<ShardsDownedSystem>().slainGenesis = true;
			ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("All bosses are slain"), Color.White, player.whoAmI);
		}
	}
}
