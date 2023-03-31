using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class DiamondCore_Greater : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;

			Item.defense = 15;

			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(0, 2, 25);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<DiamondCore>())
				.AddIngredient(ItemID.HallowedBar, 5)
				.AddIngredient(ItemID.AnkhCharm)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.ShardsOfAtheria().diamanodShield = !hideVisual;
			player.noKnockback = true;
			player.buffImmune[BuffID.Poisoned] = true;
			player.buffImmune[BuffID.Bleeding] = true;
			player.buffImmune[BuffID.Darkness] = true;
			player.buffImmune[BuffID.Cursed] = true;
			player.buffImmune[BuffID.Silenced] = true;
			player.buffImmune[BuffID.Slow] = true;
			player.buffImmune[BuffID.Confused] = true;
			player.buffImmune[BuffID.BrokenArmor] = true;
			player.buffImmune[BuffID.Weak] = true;
			player.noKnockback = true;
			player.fireWalk = true;
		}
	}
}