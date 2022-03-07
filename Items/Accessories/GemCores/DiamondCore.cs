using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class DiamondCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants immunity to knockback");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
			Item.defense = 15;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<DiamondCore_Lesser>())
				.AddIngredient(ItemID.HellstoneBar, 5)
				.AddIngredient(ItemID.CobaltShield)
				.AddTile(TileID.Anvils)
				.Register();
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.noKnockback = true;
        }
    }
}