using ShardsOfAtheria.Players;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ItemDropRules.Conditions
{
	// Very simple drop condition: drop after Skeletron's defeat
	public class DownedSkeletron : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
			{
				return NPC.downedBoss3;
			}
			return false;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return "Drops while in Slayer Mode";
		}
	}
}