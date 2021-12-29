using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items
{
	public class YellowPlushie : SpecialItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("[c/960096:'Happy Birthday friend']");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.scale = .5f;
			Item.rare = ItemRarityID.Expert;
			Item.useTime = 1;
			Item.useAnimation = 1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Shoot;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ItemID.Wire, 5)
				.AddIngredient(ItemID.Silk, 15)
				.AddIngredient(ItemID.YellowDye, 8)
				.AddIngredient(ItemID.RedDye, 2)
				.AddTile(TileID.Loom)
				.Register();
        }
    }
}