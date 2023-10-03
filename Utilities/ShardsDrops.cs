using ShardsOfAtheria.Items.BuffItems;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsDrops
    {
        public static ManyFromOptionsDropRule ManyFromOptions(int chanceDenominator, int[,] options)
        {
            return new ManyFromOptionsDropRule(chanceDenominator, 1, options);
        }

        public static void AreusCommonDrops(ref NPCLoot npcLoot)
        {
            int maxdrops = 3;
            int[,] drops = new[,]
            {
                { ModContent.ItemType<AreusShard>(), maxdrops},
                { ModContent.ItemType<Jade>(), maxdrops},
                { ItemID.GoldBar, maxdrops},
            };
            npcLoot.Add(ShardsDrops.ManyFromOptions(3, drops));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GigavoltDelicacy>(), 100));
        }
    }
}
