using SagesMania.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	public class AreusKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Unlocks your true potential\n" +
				"''Now, nothing but your own competence holds you back.''");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 22;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.Cyan;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().areusKey = true;
			player.allDamageMult += .5f;
			player.statLifeMax2 *= 2;
			player.maxRunSpeed *= 1.5f;
			player.wingTime *= 1.5f;
			player.statDefense *= 2;
			player.lifeRegen *= 2;
			player.statManaMax2 *= 2;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusOreItem>(), 7);
			recipe.AddIngredient(ItemID.ShadowKey);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}