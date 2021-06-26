using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories.GemCores
{
	public class GreaterTopazCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases max life by 40\n" +
				"Honey and Regeneration buffs");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TopazCore>());
			recipe.AddIngredient(ItemID.RegenerationPotion, 15);
			recipe.AddIngredient(ItemID.BottledHoney, 15);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statLifeMax2 += 40;
			player.AddBuff(BuffID.Regeneration, 1);
			player.AddBuff(BuffID.Honey, 1);
		}
	}
}