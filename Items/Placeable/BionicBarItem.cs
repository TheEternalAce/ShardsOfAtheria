using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;

namespace SagesMania.Items.Placeable
{
	public class BionicBarItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Bionic Bar");
			Tooltip.SetDefault("It still pulsates as if it has a heartbeat...");
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.SortingPriorityMaterials[item.type] = 59;
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = Item.sellPrice(gold: 8);
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.consumable = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 10;
			item.useAnimation = 10;
			item.createTile = ModContent.TileType<BionicBar>();
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BionicOreItem>(), 4);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 10);
			recipe.AddIngredient(ItemID.Ruby, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(ItemID.LifeCrystal, 2);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 10);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
			recipe.SetResult(ItemID.LifeFruit, 2);
			recipe.AddRecipe();
		}
	}
}