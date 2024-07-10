using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Greater
{
    public class AmberCore_Greater : ModItem
    {
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
               .AddIngredient(ModContent.ItemType<AmberCore>())
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.WarTableBanner)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().amberCape = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<AmberCore>().UpdateAccessory(player, hideVisual);
            player.maxMinions++;
            player.Gem().greaterAmberCore = true;
            player.Gem().maxAmberBanners += 10;
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
        }
    }
}