using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class BionicOreItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Bionic Ore");
			Tooltip.SetDefault("It pulsates as if it has a heartbeat...");
			ItemID.Sets.ItemIconPulse[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 9999;

			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.createTile = ModContent.TileType<BionicOre>();
			Item.consumable = true;
			Item.useTurn = true;
			Item.autoReuse = true;

			Item.value = Item.sellPrice(silver: 8);
			Item.rare = ItemRarityID.Blue;
		}
    }
}