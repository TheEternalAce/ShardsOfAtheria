﻿using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Regular
{
    public class AmberCore : ModItem
    {
        //public override void Load()
        //{
        //    if (Main.netMode != NetmodeID.Server)
        //    {
        //        EquipLoader.AddEquipTexture(Mod, "ShardsOfAtheria/Items/Accessories/GemCores/Amber", EquipType.Head, this, "Amber");
        //    }
        //}

        //public void SetupDrawing()
        //{
        //    if (Main.netMode != NetmodeID.Server)
        //    {
        //        int equipSlotHead = EquipLoader.GetEquipSlot(Mod, "AmethystMask", EquipType.Head);
        //        ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = true;
        //        ArmorIDs.Head.Sets.DrawFullHair[equipSlotHead] = true;
        //    }
        //}

        public override void SetStaticDefaults()
        {
            //SetupDrawing();
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
                .AddIngredient(ModContent.ItemType<AmberCore_Lesser>())
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.BeeWax, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().amberCape = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<AmberCore_Lesser>().UpdateAccessory(player, hideVisual);
            player.maxMinions++;
            player.Gem().amberCore = true;
            player.Gem().amberCape = !hideVisual;
        }
    }
}