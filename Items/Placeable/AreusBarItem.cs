using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;

namespace SagesMania.Items.Placeable
{
	public class AreusBarItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Areus Bar");
			Tooltip.SetDefault("Refined into a bar, it won't shock you anymore");
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
			item.createTile = ModContent.TileType<AreusBar>();
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusOreItem>(), 3);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}