using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class SapphireCore_Lesser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lesser Sapphire Core");
			Tooltip.SetDefault("5% chance to dodge attacks");
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
				.AddIngredient(ItemID.Sapphire, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SoAPlayer>().lesserSapphireCore = true;
        }
	}
}