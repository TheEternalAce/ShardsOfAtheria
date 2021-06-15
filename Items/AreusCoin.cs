using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;

namespace SagesMania.Items
{
	public class AreusCoin : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Areus refined into a coin and it's electrical powers drawn out\n" +
				"The currency of the Atherians.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Expert;
			item.maxStack = 999;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusOreItem>(), 2);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
        }
    }
}