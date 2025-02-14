using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Greater
{
    public class TopazCore_Greater : ModItem
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
                .AddIngredient(ModContent.ItemType<TopazCore>())
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.SharkToothNecklace)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().topazNecklace = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<TopazCore>().UpdateAccessory(player, hideVisual);
            player.statLifeMax2 += 20;
            player.lifeRegen += 1;
            player.Gem().greaterTopazCore = true;
            player.Gem().greaterGemCore = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer;
            var gem = player.Gem();
            if (gem.amethystCore && gem.topazCore)
            {
                TooltipLine line = new(Mod, "GemCurse", "Amethyst Curse") { OverrideColor = Color.Orange };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
            if (gem.rubyCore && gem.sapphireCore)
            {
                TooltipLine line = new(Mod, "GemCurse", "Topaz Curse") { OverrideColor = Color.Cyan };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
        }
    }
}