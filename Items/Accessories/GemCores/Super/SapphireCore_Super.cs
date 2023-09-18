﻿using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class SapphireCore_Super : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityLunaticCultist;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SapphireCore_Greater>())
                .AddIngredient(ItemID.FragmentStardust, 5)
                .AddIngredient(ItemID.FragmentVortex, 5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Shards().sapphireSpirit = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().sapphireSpirit = !hideVisual;
            player.Shards().superSapphireCore = true;
            player.maxMinions += 5;
            player.AddBuff(BuffID.Thorns, 0);
        }
    }
}