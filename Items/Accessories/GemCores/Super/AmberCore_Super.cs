using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class AmberCore_Super : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddRedemptionElement(5);
            Item.AddRedemptionElement(10);
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
                .AddIngredient(ModContent.ItemType<AmberCore_Greater>())
                .AddIngredient(ItemID.BeetleHusk, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().amberCape = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<AmberCore_Greater>().UpdateAccessory(player, hideVisual);
            player.maxMinions++;
            player.maxTurrets += 3;
            player.Gem().superAmberCore = true;
            player.Gem().superGemCore = true;
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