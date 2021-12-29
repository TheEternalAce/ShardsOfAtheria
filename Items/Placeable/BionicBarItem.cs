using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class BionicBarItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Bionic Bar");
			Tooltip.SetDefault("It still pulsates as if it has a heartbeat...");
			ItemID.Sets.ItemIconPulse[Item.type] = true;
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
			Item.createTile = ModContent.TileType<BionicBar>();
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BionicOreItem>(), 4)
				.AddTile(TileID.Furnaces)
				.Register();
		}
	}
}