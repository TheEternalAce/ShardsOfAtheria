using Terraria;
using Terraria.ID;
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
			if (player.name != "The Eternal Ace")
				return;

			sPlayer.soulCrystals = 0;
			ModContent.GetInstance<SoAWorld>().slainKing = false;
			sPlayer.KingSoul = false;
			ModContent.GetInstance<SoAWorld>().slainEOC = false;
			sPlayer.EyeSoul = false;
			ModContent.GetInstance<SoAWorld>().slainBOC = false;
			sPlayer.BrainSoul = false;
			ModContent.GetInstance<SoAWorld>().slainEOW = false;
			sPlayer.EaterSoul = false;
			ModContent.GetInstance<SoAWorld>().slainValkyrie = false;
			sPlayer.ValkyrieSoul = false;
			ModContent.GetInstance<SoAWorld>().slainBee = false;
			sPlayer.BeeSoul = false;
			ModContent.GetInstance<SoAWorld>().slainSkull = false;
			sPlayer.SkullSoul = false;
			ModContent.GetInstance<SoAWorld>().slainDeerclops = false;
			sPlayer.DeerclopsSoul = false;
			ModContent.GetInstance<SoAWorld>().slainWall = false;
			sPlayer.WallSoul = false;
			ModContent.GetInstance<SoAWorld>().slainQueen = false;
			sPlayer.QueenSoul = false;
			ModContent.GetInstance<SoAWorld>().slainMechWorm = false;
			sPlayer.DestroyerSoul = false;
			ModContent.GetInstance<SoAWorld>().slainTwins = false;
			sPlayer.TwinSoul = false;
			ModContent.GetInstance<SoAWorld>().slainPrime = false;
			sPlayer.PrimeSoul = false;
			ModContent.GetInstance<SoAWorld>().slainPlant = false;
			sPlayer.PlantSoul = false;
			ModContent.GetInstance<SoAWorld>().slainGolem = false;
			sPlayer.GolemSoul = false;
			ModContent.GetInstance<SoAWorld>().slainDuke = false;
			sPlayer.DukeSoul = false;
			ModContent.GetInstance<SoAWorld>().slainEmpress = false;
			sPlayer.EmpressSoul = false;
			ModContent.GetInstance<SoAWorld>().slainLunatic = false;
			sPlayer.LunaticSoul = false;

			ModContent.GetInstance<SoAWorld>().slainPillarNebula = false;
			ModContent.GetInstance<SoAWorld>().slainPillarSolar = false;
			ModContent.GetInstance<SoAWorld>().slainPillarStardust = false;
			ModContent.GetInstance<SoAWorld>().slainPillarVortex = false;

			ModContent.GetInstance<SoAWorld>().slainMoonLord = false;
			sPlayer.LordSoul = false;
			ModContent.GetInstance<SoAWorld>().slainSenterra = false;
			sPlayer.LandSoul = false;
			ModContent.GetInstance<SoAWorld>().slainGenesis = false;
			sPlayer.TimeSoul = false;
			ModContent.GetInstance<SoAWorld>().slainDeath = false;
			sPlayer.DeathSoul = false;
			ModContent.GetInstance<SoAWorld>().slainEverything = false;
			Main.NewText("Slayer mode reset");
		}
    }
}
