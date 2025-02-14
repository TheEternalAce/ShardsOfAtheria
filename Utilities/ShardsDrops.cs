﻿using ShardsOfAtheria.Items.BuffItems;
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
            return new(chanceDenominator, 1, options);
        }

        public static void AreusCommonDrops(ref NPCLoot npcLoot)
        {
            int[,] drops = new[,]
            {
                { ModContent.ItemType<AreusShard>(), 3},
                { ModContent.ItemType<Jade>(), 4},
                { ItemID.GoldBar, 5},
            };
            npcLoot.Add(ManyFromOptions(3, drops));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GigavoltDelicacy>(), 100));
        }
    }
}
