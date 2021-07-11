using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;

namespace SagesMania.Items.Placeable
{
	public class AreusOreItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Areus Ore"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A pale blue ore of solid electricity\n" +
				"Severely shocks you while in inventory");
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
			item.createTile = ModContent.TileType<AreusOre>();
			item.autoReuse = true;
			item.useTurn = true;
		}

        public override void UpdateInventory(Player player)
        {
			player.AddBuff(BuffID.Electrified, 5*60);
        }
    }
}