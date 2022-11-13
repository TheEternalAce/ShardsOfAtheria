using ShardsOfAtheria.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable.Furniture
{
	public class OmegaChestItem : ModItem
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
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 500;
			Item.createTile = ModContent.TileType<OmegaChest>();
			//Item.placeStyle = 1; // Use this to place the chest in its locked style
		}
	}

	public class OmegaKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 3; // Biome keys usually take 1 item to research instead.
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.GoldenKey);
			Item.width = 14;
			Item.height = 20;
			Item.maxStack = 9999;
		}
	}
}