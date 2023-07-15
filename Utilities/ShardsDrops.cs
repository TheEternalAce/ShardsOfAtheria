using ShardsOfAtheria.ShardsConditions.ItemDrop;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsDrops
    {
        public static ManyFromOptionsDropRule ManyFromOptions(int chanceDenominator, int[,] options)
        {
            return new ManyFromOptionsDropRule(chanceDenominator, 1, options);
        }
    }
}
