using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles.Crafting;

namespace ShardsOfAtheria.Items.Placeable
{
    public class PalladiumWorkbenchItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 22;
			Item.maxStack = 9999;

			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.createTile = ModContent.TileType<PalladiumWorkbench>();

			Item.rare = ItemRarityID.Cyan;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PalladiumBar, 10)
				.Register();
		}
	}
}