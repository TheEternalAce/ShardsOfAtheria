using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ItemDropRules.Conditions
{
	// Very simple drop condition: drop during daytime
	public class IsSlayerMode : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
			{
				return ModContent.GetInstance<SoAWorld>().slayerMode;
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