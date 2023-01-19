using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class EmptyNeedle : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 5);
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.IronBar, 2)
                .AddIngredient(ItemID.Glass)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}