using ShardsOfAtheria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable
{
	public class AreusShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 100;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 9999;

			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.autoReuse = true;
			Item.useTurn = true;

			Item.value = Item.sellPrice(0, 1, 50);
			Item.rare = ItemRarityID.Cyan;

			Item.createTile = ModContent.TileType<AreusOre>();
		}
	}
}