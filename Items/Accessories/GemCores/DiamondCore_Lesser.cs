using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class DiamondCore_Lesser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lesser Diamand Core");
		}
        public override string Texture => base.Texture;

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
			Item.defense = 7;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup(SoARecipes.Gold, 10)
				.AddIngredient(ItemID.Diamond, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}