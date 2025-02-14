﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Greater
{
    public class AmethystCore_Greater : ModItem
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

            Item.rare = ItemDefaults.RarityMechs;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<AmethystCore>())
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.Bomb, 15)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().amethystMask = true;
            Lighting.AddLight(player.Center, TorchID.Purple);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Gem().greaterAmethystCore = true;
            Lighting.AddLight(player.Center, TorchID.Purple);
            AmethystDashPlayer mp = player.GetModPlayer<AmethystDashPlayer>();
            mp.DashVelocity = 13f;
            ModContent.GetInstance<AmethystCore>().UpdateAccessory(player, hideVisual);
            player.Gem().greaterGemCore = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer;
            var gem = player.Gem();
            if (gem.amberCore && gem.amethystCore)
            {
                TooltipLine line = new(Mod, "GemCurse", "Amber Curse") { OverrideColor = Color.Purple };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
            if (gem.amethystCore && gem.topazCore)
            {
                TooltipLine line = new(Mod, "GemCurse", "Amethyst Curse") { OverrideColor = Color.Orange };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
        }
    }
}