using ShardsOfAtheria.Players;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ItemDropRules.Conditions
{
	// Very simple drop condition: drop after Moon Lord's defeat
	public class DownedMoonLord : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
			{
				return NPC.downedMoonlord;
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