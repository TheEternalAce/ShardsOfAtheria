using ShardsOfAtheria.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
    public class RubyCore_Super : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Ruby Core");
			Tooltip.SetDefault("15% increased damage\n" +
				"Increased knockback, attack speed and melee size\n" +
				"Attacks inflict 'Cursed Inferno' and 'Ichor'\n" +
				"Immune to damage reducing and anti attacking debuffs\n" +
				"Melee weapons autoswing");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;

			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(0, 3);
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
			player.GetAttackSpeed(DamageClass.Generic) += .15f;
			player.GetKnockback(DamageClass.Generic) += 1.5f;
			player.meleeScaleGlove = true;
			player.buffImmune[BuffID.WitheredWeapon] = true;
			player.buffImmune[BuffID.Silenced] = true;
			player.buffImmune[BuffID.Cursed] = true;
			player.buffImmune[BuffID.Weak] = true;
		}
	}
}