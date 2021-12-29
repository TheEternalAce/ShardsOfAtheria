using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class PhaseBarItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Phase Bar");
			Tooltip.SetDefault("Tears in the fabric of reality orbit it");
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 59;
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.value = Item.sellPrice(gold: 8);
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.createTile = ModContent.TileType<PhaseBar>();
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<PhaseOreItem>(), 4)
				.AddTile(TileID.Furnaces)
				.Register();
		}
	}
}