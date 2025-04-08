﻿using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Tiles.Furniture.Trophies;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable.Furniture.Trophies
{
    public class NovaTrophy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle as well as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<NovaTrophyTile>());

            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;

            Item.rare = ItemDefaults.RarityBossMasks;
            Item.value = ItemDefaults.ValueRelicTrophy;
        }
    }
}