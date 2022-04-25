using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Commands
{
    class ResetDefeated : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "resetDefeated";

		public override string Description
			=> "Reset defeated bosses";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			NPC.downedSlimeKing = false;
			NPC.downedBoss1 = false;
			NPC.downedBoss2 = false;
			SoAWorld.downedValkyrie = false;
			NPC.downedQueenBee = false;
			NPC.downedBoss3 = false;
			NPC.downedDeerclops = false;
			Main.hardMode = false;
			NPC.downedQueenSlime = false;
			NPC.downedMechBoss1 = false;
			NPC.downedMechBoss2 = false;
			NPC.downedMechBoss3 = false;
			NPC.downedPlantBoss = false;
			NPC.downedGolemBoss = false;
			NPC.downedAncientCultist = false;
			NPC.downedFishron = false;
			NPC.downedEmpressOfLight = false;
			NPC.downedMoonlord = false;
			Main.NewText("Bosses have no longer been defeated");
		}
    }
}
