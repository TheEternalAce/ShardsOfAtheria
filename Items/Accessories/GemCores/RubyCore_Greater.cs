using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class RubyCore_Greater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greater Ruby Core");
			Tooltip.SetDefault("10% increased damage\n" +
				"Increases attack speed by 10%\n" +
				"Increases knockback\n" +
				"Increased melee size\n" +
				"Attacks inflict 'Hellfire'\n" +
				"Melee weapons autoswing");
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
				.AddIngredient(ItemID.HallowedBar, 5)
				.AddIngredient(ItemID.FireGauntlet)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += .1f;
			player.autoReuseGlove = true;
			player.GetAttackSpeed(DamageClass.Generic) += .1f;
			player.GetKnockback(DamageClass.Generic) += 2;
			player.meleeScaleGlove = true;
			player.GetModPlayer<SoAPlayer>().greaterRubyCore = true;
		}
	}
}