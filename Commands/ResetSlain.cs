using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
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
			ModContent.GetInstance<SoAWorld>().slainValkyrie = false;
			ModContent.GetInstance<SoAWorld>().slainEOC = false;
			ModContent.GetInstance<SoAWorld>().slainBOC = false;
			ModContent.GetInstance<SoAWorld>().slainEOW = false;
			ModContent.GetInstance<SoAWorld>().slainBee = false;
			ModContent.GetInstance<SoAWorld>().slainSkull = false;
			ModContent.GetInstance<SoAWorld>().slainWall = false;
			ModContent.GetInstance<SoAWorld>().slainMechWorm = false;
			ModContent.GetInstance<SoAWorld>().slainTwins = false;
			ModContent.GetInstance<SoAWorld>().slainPrime = false;
			ModContent.GetInstance<SoAWorld>().slainPlant = false;
			ModContent.GetInstance<SoAWorld>().slainGolem = false;
			ModContent.GetInstance<SoAWorld>().slainDuke = false;
			ModContent.GetInstance<SoAWorld>().slainEmpress = false;
			ModContent.GetInstance<SoAWorld>().slainMoonLord = false;
			ModContent.GetInstance<SoAWorld>().slainSenterra = false;
			ModContent.GetInstance<SoAWorld>().slainGenesis = false;
			ModContent.GetInstance<SoAWorld>().slainEverything = false;
			ModContent.GetInstance<SoAWorld>().messageToPlayer = 0;
			Main.LocalPlayer.ClearBuff(ModContent.BuffType<Buffs.AwakenedSlayer>());
			Main.NewText("Bosses can now be summoned again");
		}
    }
}
