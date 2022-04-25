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
			=> "resetSlayer";

		public override string Description
			=> "Reset Slayer mode related variables";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			SlayerPlayer player = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();

			player.anySoulCrystals = false;
			ModContent.GetInstance<SoAWorld>().slainKing = false;
			player.KingSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainEOC = false;
			player.EyeSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainBOC = false;
			player.BrainSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainEOW = false;
			player.EaterSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainValkyrie = false;
			player.ValkyrieSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainBee = false;
			player.BeeSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainSkull = false;
			player.SkullSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainDeerclops = false;
			player.DeerclopsSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainWall = false;
			player.WallSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainQueen = false;
			player.QueenSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainMechWorm = false;
			player.DestroyerSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainTwins = false;
			player.TwinSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainPrime = false;
			player.PrimeSoul = 0;
			player.blueprintRead = false;
			ModContent.GetInstance<SoAWorld>().slainPlant = false;
			player.PlantSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainGolem = false;
			player.GolemSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainDuke = false;
			player.DukeSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainEmpress = false;
			player.EmpressSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainMoonLord = false;
			player.LordSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainSenterra = false;
			player.LandSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainGenesis = false;
			player.TimeSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainDeath = false;
			player.DeathSoul = 0;
			ModContent.GetInstance<SoAWorld>().slainEverything = false;
			ModContent.GetInstance<SoAWorld>().messageToPlayer = 0;
			Main.LocalPlayer.ClearBuff(ModContent.BuffType<Buffs.AwakenedSlayer>());
			Main.NewText("Slayer mode reset");
		}
    }
}
