using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Commands
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
			ModContent.GetInstance<SMWorld>().slainValkyrie = true;
			ModContent.GetInstance<SMWorld>().slainEOC = true;
			ModContent.GetInstance<SMWorld>().slainBOC = true;
			ModContent.GetInstance<SMWorld>().slainEOW = true;
			ModContent.GetInstance<SMWorld>().slainBee = true;
			ModContent.GetInstance<SMWorld>().slainSkull = true;
			ModContent.GetInstance<SMWorld>().slainWall = true;
			ModContent.GetInstance<SMWorld>().slainMechWorm = true;
			ModContent.GetInstance<SMWorld>().slainTwins = true;
			ModContent.GetInstance<SMWorld>().slainPrime = true;
			ModContent.GetInstance<SMWorld>().slainPlant = true;
			ModContent.GetInstance<SMWorld>().slainGolem = true;
			ModContent.GetInstance<SMWorld>().slainDuke = true;
			ModContent.GetInstance<SMWorld>().slainEmpress = true;
			ModContent.GetInstance<SMWorld>().slainMoonLord = true;
			ModContent.GetInstance<SMWorld>().slainSenterra = true;
			ModContent.GetInstance<SMWorld>().slainGenesis = true;
			ModContent.GetInstance<SMWorld>().slainEverything = true;
			Main.NewText("All bosses are slain");
		}
    }
}
