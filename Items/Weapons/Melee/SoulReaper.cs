using ShardsOfAtheria.Items.Weapons.Ranged;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class SoulReaper : SpecialItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'A weapon that once belonged to the ancient Grimm Reaper'");
		}

		public override void SetDefaults()
		{
			Item.damage = 1000;
			Item.DamageType = DamageClass.Melee;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(gold: 50);
			Item.rare = ItemRarityID.Expert;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.useTurn = true;
			Item.crit = 100;
		}

        public override void HoldItem(Player player)
        {
			player.statLifeMax2 = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BlackAreusSword>())
				.AddIngredient(ModContent.ItemType<AreusDagger>())
				.AddIngredient(ModContent.ItemType<CrossDagger>())
				.AddIngredient(ModContent.ItemType<AreusPistol>())
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}