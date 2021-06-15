using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;

namespace SagesMania.Items.Placeable
{
	public class GlitchItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("gElRiRtOcRh");
			Tooltip.SetDefault("The bane of every programer's existance");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Gray;
			item.maxStack = 999;
			item.consumable = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 10;
			item.useAnimation = 10;
			item.createTile = ModContent.TileType<GlitchTile>();
			item.autoReuse = true;
		}

        public override void UpdateInventory(Player player)
        {
			player.AddBuff(BuffID.Confused, 5*60);
        }
    }
}