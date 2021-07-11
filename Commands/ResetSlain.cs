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
			NPC.downedBoss1 = false;
			NPC.downedBoss2 = false;
			NPC.downedQueenBee = false;
			NPC.downedBoss3 = false;
			Main.hardMode = false;
			ModContent.GetInstance<SMWorld>().slainEOC = false;
			ModContent.GetInstance<SMWorld>().slainBOC = false;
			ModContent.GetInstance<SMWorld>().slainEOW = false;
			ModContent.GetInstance<SMWorld>().slainBee = false;
			ModContent.GetInstance<SMWorld>().slainSkull = false;
			ModContent.GetInstance<SMWorld>().slainWall = false;
			Main.NewText("Bosses can now be summoned again");
			Main.NewText("If world is hardmode you will need to defeat Wall of Flesh again");
		}
    }
}
