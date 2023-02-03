﻿using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class DiamondCore_Super : ModItem
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

			Item.defense = 20;

			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(0, 3);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<DiamondCore_Greater>())
				.AddIngredient(ItemID.FragmentSolar, 5)
				.AddIngredient(ItemID.FragmentVortex, 5)
				.AddIngredient(ItemID.AnkhShield)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.ShardsOfAtheria().diamanodShield = true;
			if (player.statLife <= player.statLifeMax * 0.5)
			{
				player.AddBuff(62, 5);
			}

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

			player.buffImmune[BuffID.WitheredArmor] = true;
			player.buffImmune[BuffID.Ichor] = true;
		}
	}
}