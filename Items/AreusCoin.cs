using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items
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
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Cyan;
			Item.maxStack = 999;
		}

        public override void AddRecipes()
        {
			CreateRecipe(5)
				.AddIngredient(ModContent.ItemType<AreusOreItem>(), 2)
				.AddTile(ModContent.TileType<AreusForge>())
				.Register();
        }
    }
}