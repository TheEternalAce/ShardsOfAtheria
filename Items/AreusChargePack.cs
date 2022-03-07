using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using ShardsOfAtheria;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ShardsOfAtheria.Items
{
    class AreusChargePack : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click an areus weapon to increase it's charge by 50");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 10);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.NPCHit53;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.consumable = true;
            Item.maxStack = 30;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusOreItem>())
                .AddTile(ModContent.TileType<AreusForge>())
                .Register();
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SoAPlayer>().areusChargePack = true;
        }
    }
}
