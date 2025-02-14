using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Regular
{
    public class SapphireCore : ModItem
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

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SapphireCore_Lesser>())
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.Bone, 20)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().sapphireSpirit = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Gem().sapphireSpirit = !hideVisual;
            ModContent.GetInstance<SapphireCore_Lesser>().UpdateAccessory(player, hideVisual);
            player.Gem().sapphireDodgeChance += 0.05f;
            player.Gem().sapphireCore = true;
            player.Gem().gemCore = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer;
            var gem = player.Gem();
            if (gem.rubyCore && gem.sapphireCore)
            {
                TooltipLine line = new(Mod, "GemCurse", "Sapphire Curse") { OverrideColor = Color.Red };
                tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
            }
        }
    }
}