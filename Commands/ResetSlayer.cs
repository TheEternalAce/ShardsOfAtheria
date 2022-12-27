using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
    class ResetSlayer : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "resetSlayer";

		public override string Description
			=> "Reset Slayer mode related variables";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			SlayerPlayer sPlayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
			Player player = Main.LocalPlayer;

			sPlayer.soulCrystals.Clear();
			ModContent.GetInstance<ShardsDownedSystem>().slainKing = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainEOC = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainBOC = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainEOW = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainBee = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainSkull = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainDeerclops = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainWall = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainQueen = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainMechWorm = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainTwins = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainPrime = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainPlant = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainGolem = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainDuke = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainEmpress = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainLunatic = false;

			ModContent.GetInstance<ShardsDownedSystem>().slainPillarNebula = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainPillarSolar = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainPillarStardust = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainPillarVortex = false;

			ModContent.GetInstance<ShardsDownedSystem>().slainMoonLord = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainSenterra = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainGenesis = false;
			ModContent.GetInstance<ShardsDownedSystem>().slainDeath = false;
			ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("Slayer mode reset"), Color.White, player.whoAmI);
		}
	}
}
