using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Commands
{
    class ResetSlain : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "resetSlain";

		public override string Description
			=> "Reset slain bosses";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			ModContent.GetInstance<SMWorld>().message = 0;
			ModContent.GetInstance<SMWorld>().slainValkyrie = false;
			ModContent.GetInstance<SMWorld>().slainEOC = false;
			ModContent.GetInstance<SMWorld>().slainBOC = false;
			ModContent.GetInstance<SMWorld>().slainEOW = false;
			ModContent.GetInstance<SMWorld>().slainBee = false;
			ModContent.GetInstance<SMWorld>().slainSkull = false;
			ModContent.GetInstance<SMWorld>().slainWall = false;
			ModContent.GetInstance<SMWorld>().slainMechWorm = false;
			ModContent.GetInstance<SMWorld>().slainTwins = false;
			ModContent.GetInstance<SMWorld>().slainPrime = false;
			ModContent.GetInstance<SMWorld>().slainPlant = false;
			ModContent.GetInstance<SMWorld>().slainGolem = false;
			ModContent.GetInstance<SMWorld>().slainDuke = false;
			ModContent.GetInstance<SMWorld>().slainEmpress = false;
			ModContent.GetInstance<SMWorld>().slainMoonLord = false;
			ModContent.GetInstance<SMWorld>().slainSenterra = false;
			ModContent.GetInstance<SMWorld>().slainGenesis = false;
			ModContent.GetInstance<SMWorld>().slainEverything = false;
			Main.NewText("Bosses can now be summoned again");
		}
    }
}
