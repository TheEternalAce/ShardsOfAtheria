using Terraria;
using Terraria.ID;
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
			if (player.name != "Tester Shane")
				return;

			ModContent.GetInstance<SoAWorld>().slainValkyrie = true;
			ModContent.GetInstance<SoAWorld>().slainEOC = true;
			ModContent.GetInstance<SoAWorld>().slainBOC = true;
			ModContent.GetInstance<SoAWorld>().slainEOW = true;
			ModContent.GetInstance<SoAWorld>().slainBee = true;
			ModContent.GetInstance<SoAWorld>().slainSkull = true;
			ModContent.GetInstance<SoAWorld>().slainWall = true;
			ModContent.GetInstance<SoAWorld>().slainMechWorm = true;
			ModContent.GetInstance<SoAWorld>().slainTwins = true;
			ModContent.GetInstance<SoAWorld>().slainPrime = true;
			ModContent.GetInstance<SoAWorld>().slainPlant = true;
			ModContent.GetInstance<SoAWorld>().slainGolem = true;
			ModContent.GetInstance<SoAWorld>().slainDuke = true;
			ModContent.GetInstance<SoAWorld>().slainEmpress = true;
			ModContent.GetInstance<SoAWorld>().slainMoonLord = true;
			ModContent.GetInstance<SoAWorld>().slainSenterra = true;
			ModContent.GetInstance<SoAWorld>().slainGenesis = true;
			ModContent.GetInstance<SoAWorld>().slainEverything = true;
			Main.NewText("All bosses are slain");
		}
    }
}
