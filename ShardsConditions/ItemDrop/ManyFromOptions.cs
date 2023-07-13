using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace ShardsOfAtheria.ShardsConditions.ItemDrop
{
    internal class ManyFromOptionsDropRule : IItemDropRule
    {
        public int[,] dropIds;
        public int chanceDenominator;
        public int chanceNumerator;

        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

        public ManyFromOptionsDropRule(int chanceDenominator, int chanceNumerator, int[,] options)
        {
            this.chanceDenominator = chanceDenominator;
            this.chanceNumerator = chanceNumerator;
            dropIds = options;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
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
                int ind = info.rng.Next(dropIds.Length / 2);
                int amount = Main.rand.Next(dropIds[ind, 1] + 1);
                CommonCode.DropItem(info, dropIds[ind, 0], amount);
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
            float dropRate = 1f / dropIds.Length / 2 * num2;
            for (int i = 0; i < dropIds.Length / 2; i++)
            {
                drops.Add(new DropRateInfo(dropIds[i, 0], 1, dropIds[i, 1], dropRate, ratesInfo.conditions));
            }

            Chains.ReportDroprates(ChainedRules, num, drops, ratesInfo);
        }
    }
}
