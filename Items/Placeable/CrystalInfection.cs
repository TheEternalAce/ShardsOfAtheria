using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;

namespace SagesMania.Items.Placeable
{
	public class CrystalInfection : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("A chunk of crystalized infection");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 8);
			item.rare = ItemRarityID.Expert;
			item.maxStack = 999;
			item.consumable = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 10;
			item.useAnimation = 10;
			item.createTile = ModContent.TileType<InfectionCrystal>();
			item.autoReuse = true;
		}
    }
}