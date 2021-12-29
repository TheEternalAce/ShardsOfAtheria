using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
	public class EmptyNeedle : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = ItemRarityID.White;
			Item.maxStack = 99;
		}

        public override void AddRecipes()
        {
			CreateRecipe(5)
				.AddRecipeGroup(RecipeGroupID.IronBar, 2)
				.AddIngredient(ItemID.Glass)
				.AddTile(TileID.WorkBenches)
				.Register();
        }
    }
}