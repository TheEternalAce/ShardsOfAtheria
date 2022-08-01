using Terraria;
using Terraria.GameContent.Creative;
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

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999;

			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.createTile = ModContent.TileType<BionicBar>();
			Item.consumable = true;
			Item.useTurn = true;
			Item.autoReuse = true;

			Item.value = Item.sellPrice(0,  1);
			Item.rare = ItemRarityID.Blue;
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