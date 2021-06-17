using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;

namespace SagesMania.Items
{
	public class SlitheryLand: ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons the Atherial Land\n" +
				"Eventually");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Red;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusOreItem>(), 5);
			recipe.AddIngredient(ItemID.SpiderFang, 8);
			recipe.AddIngredient(ItemID.GoldWatch);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}