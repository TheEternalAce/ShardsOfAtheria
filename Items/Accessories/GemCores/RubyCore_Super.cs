using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class RubyCore_Super : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Ruby Core");
			Tooltip.SetDefault("15% increased damage and melee speed\n" +
				"Increased melee knockback and swing speed\n" +
				"Attacks inflict 'Cursed Inferno' and 'Ichor'\n" +
				"Immune to damage reducing and anti attacking debuffs\n" +
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
				.AddIngredient(ModContent.ItemType<RubyCore_Greater>())
				.AddIngredient(ItemID.FragmentSolar, 5)
				.AddIngredient(ItemID.FragmentNebula, 5)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += .15f;
			player.GetModPlayer<SoAPlayer>().superRubyCore = true;
			player.autoReuseGlove = true;
			player.meleeSpeed += .15f;
			player.kbGlove = true;
			player.meleeScaleGlove = true;
			player.buffImmune[BuffID.WitheredWeapon] = true;
			player.buffImmune[BuffID.Silenced] = true;
			player.buffImmune[BuffID.Cursed] = true;
			player.buffImmune[BuffID.Weak] = true;
		}
	}
}