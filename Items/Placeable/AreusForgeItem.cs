using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class AreusForgeItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Areus Forge");
			Tooltip.SetDefault("Used to make things out of Areus");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 22;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.rare = ItemRarityID.Cyan;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AreusForge>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusOreItem>(), 10)
				.AddRecipeGroup(RecipeGroupID.Wood, 5)
				.AddIngredient(ItemID.Torch, 5)
				.Register();
		}
	}
}