using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Placeable
{
	public class GlitchItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("gElRiRtOcRh");
			Tooltip.SetDefault("The bane of every programmer's existence");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Gray;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.createTile = ModContent.TileType<GlitchTile>();
			Item.autoReuse = true;
		}

        public override void UpdateInventory(Player player)
        {
			player.AddBuff(BuffID.Confused, 5*60);
        }
    }
}