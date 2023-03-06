using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable.Crafting
{
    public class AreusFabricatorItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.createTile = ModContent.TileType<AreusFabricator>();
            Item.consumable = true;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(silver: 8);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ModContent.ItemType<AreusShard>(), 15)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}