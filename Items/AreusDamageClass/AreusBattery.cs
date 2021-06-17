using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;

namespace SagesMania.Items.AreusDamageClass
{
	public class AreusBattery: ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("All attacks inflict Electrified\n" +
				"Grants immunity to Electrified\n" +
				"Increases Areus Charge by 50 and Areus Charge regenerates");
		}

		public override void SetDefaults()
		{
			item.width = 15;
			item.height = 20;
			item.value = Item.sellPrice(gold: 15);
			item.rare = ItemRarityID.Cyan;
			item.accessory = true;
		}
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[BuffID.Electrified] = true;
			player.GetModPlayer<SMPlayer>().areusBatteryElectrify = true;
			player.GetModPlayer<SMPlayer>().naturalAreusRegen = true;
			player.GetModPlayer<AreusDamagePlayer>().areusResourceMax2 += 50;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusOreItem>(), 5);
			recipe.AddIngredient(ItemID.Leather, 10);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}