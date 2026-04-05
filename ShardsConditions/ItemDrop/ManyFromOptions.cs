using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace ShardsOfAtheria.ShardsConditions.ItemDrop
{
    public struct ItemDrop(int type, int count, Condition condition = null)
    {
        public int Type = type;
        public int Count = count;
        public Condition Condition = condition;
    }

    public class ManyFromOptionsDropRule(int chanceDenominator, int chanceNumerator, List<ItemDrop> options) : IItemDropRule
    {
        public static readonly int IndexItemID = 0;
        public static readonly int IndexItemCount = 1;

        public List<ItemDrop> drops = options;
        public int chanceDenominator = chanceDenominator;
        public int chanceNumerator = chanceNumerator;

        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; } = [];

        List<ItemDrop> GetDropableItems()
        {
            List<ItemDrop> result = [];
            foreach (ItemDrop drop in drops)
            {
                if (drop.Condition == null || drop.Condition.IsMet()) result.Add(drop);
            }
            return result;
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            return true;
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result;
            if (info.player.RollLuck(chanceDenominator) < chanceNumerator)
            {
                var drops = GetDropableItems();
                int ind = info.rng.Next(drops.Count);
                var drop = drops[ind];
                int amount = Main.rand.Next(drop.Count + 1);
                CommonCode.DropItem(info, drop.Type, amount);
                result = default;
                result.State = ItemDropAttemptResultState.Success;
                return result;
            }

            result = default;
            result.State = ItemDropAttemptResultState.FailedRandomRoll;
            return result;
        }

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            float num = chanceNumerator / (float)chanceDenominator;
            float num2 = num * ratesInfo.parentDroprateChance;
            float dropRate = 1f / this.drops.Count * num2;
            for (int i = 0; i < this.drops.Count; i++)
            {
                var drop = this.drops[i];
                drops.Add(new DropRateInfo(drop.Type, 1, drops.Count, dropRate, ratesInfo.conditions));
            }

            Chains.ReportDroprates(ChainedRules, num, drops, ratesInfo);
        }
    }
}
