using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;

namespace SagesMania.Items
{
	public class Dryskal : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Gold and cobwebs merged into a coin\n" +
				"The currency of the Sil.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SM:GoldBars");
			recipe.AddIngredient(ItemID.Cobweb);
			recipe.AddTile(TileID.Loom);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
        }
    }
}