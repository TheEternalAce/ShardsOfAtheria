using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items
{
	public class EmptyNeedle : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 48;
			item.value = Item.sellPrice(silver: 20);
			item.rare = ItemRarityID.White;
			item.maxStack = 99;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.AddIngredient(ItemID.Glass);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
        }
    }
}