using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class TopazCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases max Life by 40");
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
				.AddIngredient(ModContent.ItemType<TopazCore_Lesser>())
				.AddIngredient(ItemID.HellstoneBar, 5)
				.AddIngredient(ItemID.LifeCrystal)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statLifeMax2 += 40;
        }
	}
}