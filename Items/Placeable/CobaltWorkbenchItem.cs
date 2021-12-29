using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class CobaltWorkbenchItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Cobalt Workbench");
			Tooltip.SetDefault("Used to completely skip having to gather mythril ore\n" +
				"[c/960096:'Why? Because $*#% mythril!']");
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
			Item.createTile = ModContent.TileType<CobaltWorkbench>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CobaltBar, 10)
				.Register();
		}
	}
}