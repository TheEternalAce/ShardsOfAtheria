using Terraria.GameContent.Creative;
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
			Tooltip.SetDefault("Used to completely skip having to gather mythril ore");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
			Item.createTile = ModContent.TileType<CobaltWorkbench>();

			Item.rare = ItemRarityID.Cyan;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CobaltBar, 10)
				.Register();
		}
	}
}