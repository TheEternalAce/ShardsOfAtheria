using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace ShardsOfAtheria.ShardsConditions.ItemDrop
{
    // Very simple drop condition: drop while the player is on the surface during the night
    public class IsInTwilight : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation) return !Main.IsItDay() && info.player.ZoneOverworldHeight;
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return ShardsHelpers.LocalizeCondition("Twilight");
        }
    }
}