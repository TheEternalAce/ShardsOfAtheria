using Terraria.ModLoader;
using Terraria.ID;
using SagesMania.Walls;
using SagesMania.Items.Placeable;

namespace SagesMania.Items.Placeable
{
	public class ExampleWallItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/960096:''An example wall to give me an idea of what I'm doing'']");
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 7;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createWall = ModContent.WallType<ExampleWall>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ExampleBlockItem>());
			recipe.SetResult(this, 4);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock);
			recipe.SetResult(this, 4);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 4);
			recipe.SetResult(ItemID.DirtBlock);
			recipe.AddRecipe();
		}
	}
}