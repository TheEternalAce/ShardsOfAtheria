using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class CrystalInfection : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("A chunk of crystallized infection");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(copper: 10);
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.createTile = ModContent.TileType<InfectionCrystal>();
			Item.autoReuse = true;
		}
    }
}