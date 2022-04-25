using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class EmeraldCore_Lesser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lesser Emerald Core");
			Tooltip.SetDefault("5% increased movement speed\n" +
				"Increased jump height");
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
				.AddRecipeGroup(SoARecipes.Gold, 10)
				.AddIngredient(ItemID.Emerald, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.jumpBoost = true;
			player.GetModPlayer<SoAPlayer>().lesserEmeraldCore = true;
		}
	}
}