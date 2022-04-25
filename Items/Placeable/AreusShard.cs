using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.Placeable
{
	public class AreusShard : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("A pale blue shard of limitless electricity");

			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 11));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(0,  1, silver: 50);
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