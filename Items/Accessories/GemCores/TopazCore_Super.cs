using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class TopazCore_Super : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Topaz Core");
			Tooltip.SetDefault("Increases max life by 60\n" +
				"Honey, Regeneration, Cozy Campfire and Heart Lantern buffs");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<TopazCore_Greater>())
				.AddIngredient(ItemID.FragmentSolar, 5)
				.AddIngredient(ItemID.FragmentNebula, 5)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statLifeMax2 += 60;
			player.shinyStone = true;
			player.AddBuff(BuffID.Regeneration, 2);
			player.AddBuff(BuffID.Honey, 2);
			player.AddBuff(BuffID.Campfire, 2);
			player.AddBuff(BuffID.HeartLamp, 2);
		}
	}
}