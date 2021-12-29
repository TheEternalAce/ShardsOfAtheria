using Terraria.ModLoader;
using Terraria.ID;
using ShardsOfAtheria.Walls;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Items.Placeable
{
	public class ExampleWallItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/960096:'An example wall to give me an idea of what I'm doing']");
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 7;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createWall = ModContent.WallType<ExampleWall>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ExampleBlockItem>())
				.Register();
		}
	}
}