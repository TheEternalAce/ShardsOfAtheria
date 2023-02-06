using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Potions
{
	public class ChargedFlightPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 30;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 50;
			Item.maxStack = 9999;

			Item.DefaultToPotion(ModContent.BuffType<ChargedFlight>(), 28800);

			Item.value = 7500;
			Item.rare = ItemRarityID.Green;

		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ChargedFeather>())
				.AddIngredient(ModContent.ItemType<AreusShard>())
				.AddIngredient(ItemID.BottledWater)
				.AddTile(TileID.Bottles)
				.Register();
		}
	}

	public class ChargedFlight : ModBuff
	{

	}

	public class ChargedFlightPlayer : ModPlayer
	{
		public override void PostUpdateEquips()
		{
			if (Player.HasBuff(ModContent.BuffType<ChargedFlight>()))
			{
				Player.wingTimeMax += 34;
			}
		}
	}
}