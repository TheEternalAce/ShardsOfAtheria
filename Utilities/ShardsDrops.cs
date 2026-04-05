using ShardsOfAtheria.Items.BuffItems;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsDrops
    {
        public static ManyFromOptionsDropRule ManyFromOptions(int chanceDenominator, List<ItemDrop> options)
        {
            return new(chanceDenominator, 1, options);
        }

        public static void AreusCommonDrops(ref NPCLoot npcLoot)
        {
            List<ItemDrop> drops =
            [
                new(ModContent.ItemType<AreusShard>(), 3),
                new(ModContent.ItemType<Jade>(), 4),
                new(ItemID.GoldBar, 5),
            ];
            npcLoot.Add(ManyFromOptions(3, drops));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GigavoltDelicacy>(), 100));
        }
    }
}
