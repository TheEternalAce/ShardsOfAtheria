﻿using ShardsOfAtheria.Utilities;
using Terraria.GameContent.ItemDropRules;

namespace ShardsOfAtheria.ShardsConditions.ItemDrop
{
    // Very simple drop condition: drop in eternity expert mode
    public class Eternity : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!SoA.Eternity()) return false;
            return true;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return ShardsHelpers.LocalizeCondition("Eternity");
        }
    }
}