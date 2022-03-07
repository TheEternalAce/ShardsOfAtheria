using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class PhaseOreItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Phase Ore");
			ItemID.Sets.ItemIconPulse[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 8);
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.createTile = ModContent.TileType<PhaseOre>();
			Item.autoReuse = true;
		}
    }
}