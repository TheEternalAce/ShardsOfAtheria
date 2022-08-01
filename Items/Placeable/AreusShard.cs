using ShardsOfAtheria.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable
{
	public class AreusShard : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("A pale blue shard of limitless electricity");

			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 11));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
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