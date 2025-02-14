using ShardsOfAtheria.Utilities;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace ShardsOfAtheria.ShardsConditions.ItemDrop
{
    // Very simple drop condition: drop in slayer mode
    public class IsSlayerMode : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return info.player.Slayer().slayerMode;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return ShardsHelpers.LocalizeCondition("Slayer");
        }
    }
}