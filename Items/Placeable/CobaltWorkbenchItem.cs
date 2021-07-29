using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;

namespace SagesMania.Items.Placeable
{
	public class CobaltWorkbenchItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Cobalt Workbench");
			Tooltip.SetDefault("Used to completely skip having to gather mythril ore");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 22;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.rare = ItemRarityID.Cyan;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<CobaltWorkbench>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltBar, 10);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}