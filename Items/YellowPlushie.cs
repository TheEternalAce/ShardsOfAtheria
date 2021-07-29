using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;

namespace SagesMania.Items
{
	public class YellowPlushie : SpecialItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("[c/960096:''Happy Birthday friend'']");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.scale = .5f;
			item.rare = ItemRarityID.Expert;
			item.useTime = 1;
			item.useAnimation = 1;
			item.autoReuse = true;
			item.useTurn = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wire, 5);
			recipe.AddIngredient(ItemID.Silk, 15);
			recipe.AddIngredient(ItemID.YellowDye, 8);
			recipe.AddIngredient(ItemID.RedDye, 2);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}