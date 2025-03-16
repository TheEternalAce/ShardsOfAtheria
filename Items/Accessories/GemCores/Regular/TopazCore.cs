﻿using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Regular
{
    public class TopazCore : ModItem
    {
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, "ShardsOfAtheria/Items/Accessories/GemCores/TopazAmulet", EquipType.Neck, this, "TopazAmulet");
            }
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TopazCore_Lesser>())
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.LifeCrystal)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().topazNecklace = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Gem().topazNecklace = !hideVisual;
            player.Gem().topazCore = true;
            ModContent.GetInstance<TopazCore_Lesser>().UpdateAccessory(player, hideVisual);
            player.statLifeMax2 += 20;
            player.Gem().gemCore = true;
        }
    }
}