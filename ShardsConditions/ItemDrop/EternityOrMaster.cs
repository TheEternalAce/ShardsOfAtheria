using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace ShardsOfAtheria.ShardsConditions.ItemDrop
{
    // Very simple drop condition: drop in eternity expert mode
    public class EternityOrMaster : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!SoA.Eternity())
            {
                if (Main.masterMode) return true;
                else return false;
            }
            if (!Main.expertMode) return false;
            return true;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return Language.GetTextValue("Mods.ShardsOfAtheria.Conditions.EternityExpertOrMaster");
        }
    }
}