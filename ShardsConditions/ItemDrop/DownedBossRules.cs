﻿using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace ShardsOfAtheria.ShardsConditions.ItemDrop
{
    // Very simple drop condition: drop after Nova's defeat
    public class DownedValkyrie : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return ShardsDownedSystem.downedValkyrie;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return Language.GetTextValue("Mods.ShardsOfAtheria.Condition.DownedNova");
        }
    }

    // Very simple drop condition: drop after Golem's defeat
    public class DownedGolem : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return NPC.downedGolemBoss;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return Language.GetTextValue("Mods.ShardsOfAtheria.Condition.PostGolem");
        }
    }

    // Very simple drop condition: drop after Lunatic Cultist's defeat
    public class DownedLunaticCultist : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return NPC.downedAncientCultist;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return Language.GetTextValue("Mods.ShardsOfAtheria.Condition.PostCultist");
        }
    }
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
            return Language.GetTextValue("Mods.ShardsOfAtheria.Condition.PostMoonLord");
        }
    }
}