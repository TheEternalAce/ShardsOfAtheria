using ShardsOfAtheria.Utilities;
using Terraria.GameContent.ItemDropRules;

namespace ShardsOfAtheria.ShardsConditions.ItemDrop
{
    // Very simple drop condition: drop while it is raining
    public class IsInUnderworld : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation) return info.player.ZoneOverworldHeight;
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return ShardsHelpers.LocalizeCondition("Underworld");
        }
    }
}