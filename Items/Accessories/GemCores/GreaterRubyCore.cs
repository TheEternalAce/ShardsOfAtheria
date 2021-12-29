using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class GreaterRubyCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("10% increased damage\n" +
				"Attacks inflict 'On Fire'");
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
				.AddIngredient(ModContent.ItemType<RubyCore>())
				.AddIngredient(ItemID.Hellstone, 10)
				.AddIngredient(ItemID.FeralClaws)
				.AddTile(TileID.Hellforge)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += .1f;
			player.GetModPlayer<SMPlayer>().greaterRubyCore = true;
		}
	}
}