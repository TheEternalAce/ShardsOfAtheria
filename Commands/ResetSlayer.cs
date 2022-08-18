using ShardsOfAtheria.Players;
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
			ModContent.GetInstance<SoADownedSystem>().slainKing = false;
			sPlayer.KingSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainEOC = false;
			sPlayer.EyeSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainBOC = false;
			sPlayer.BrainSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainEOW = false;
			sPlayer.EaterSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainValkyrie = false;
			sPlayer.ValkyrieSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainBee = false;
			sPlayer.BeeSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainSkull = false;
			sPlayer.SkullSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainDeerclops = false;
			sPlayer.DeerclopsSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainWall = false;
			sPlayer.WallSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainQueen = false;
			sPlayer.QueenSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainMechWorm = false;
			sPlayer.DestroyerSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainTwins = false;
			sPlayer.TwinSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainPrime = false;
			sPlayer.PrimeSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainPlant = false;
			sPlayer.PlantSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainGolem = false;
			sPlayer.GolemSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainDuke = false;
			sPlayer.DukeSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainEmpress = false;
			sPlayer.EmpressSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainLunatic = false;
			sPlayer.LunaticSoul = false;

			ModContent.GetInstance<SoADownedSystem>().slainPillarNebula = false;
			ModContent.GetInstance<SoADownedSystem>().slainPillarSolar = false;
			ModContent.GetInstance<SoADownedSystem>().slainPillarStardust = false;
			ModContent.GetInstance<SoADownedSystem>().slainPillarVortex = false;

			ModContent.GetInstance<SoADownedSystem>().slainMoonLord = false;
			sPlayer.LordSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainSenterra = false;
			sPlayer.LandSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainGenesis = false;
			sPlayer.TimeSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainDeath = false;
			sPlayer.DeathSoul = false;
			ModContent.GetInstance<SoADownedSystem>().slainEverything = false;
			Main.NewText("Slayer mode reset");
		}
    }
}
