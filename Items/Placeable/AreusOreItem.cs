using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class AreusOreItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Areus Ore"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A pale blue ore of solid electricity");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(gold: 1, silver: 50);
			Item.rare = ItemRarityID.Cyan;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.createTile = ModContent.TileType<AreusOre>();
			Item.autoReuse = true;
			Item.useTurn = true;
		}
    }
}