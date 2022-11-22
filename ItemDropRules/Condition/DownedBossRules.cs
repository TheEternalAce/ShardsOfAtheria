using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

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
            return Language.GetTextValue("Mods.ShardsOfAtheria.DropCondition.PostSkeletron");
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
            return Language.GetTextValue("Mods.ShardsOfAtheria.DropCondition.PostGolem");
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
            return Language.GetTextValue("Mods.ShardsOfAtheria.DropCondition.PostCultist");
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
            return Language.GetTextValue("Mods.ShardsOfAtheria.DropCondition.PostMoonLord");
        }
    }
}