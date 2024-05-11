using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable.Furniture
{
    public class NullFieldGenerator : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.createTile = ModContent.TileType<AreusNullFieldGenerator>();
            Item.consumable = true;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ModContent.ItemType<Jade>(), 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}