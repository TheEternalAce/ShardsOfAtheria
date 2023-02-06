using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Potions
{
	public class ConductivityPotion : ModItem
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

			Item.DefaultToPotion(ModContent.BuffType<Conductive>(), 14400);

			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.Cyan;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>())
				.AddIngredient(ItemID.CopperOre)
				.AddIngredient(ItemID.BottledWater)
				.AddTile(TileID.Bottles)
				.Register();
		}
	}

	public class Conductive : ModBuff
	{
		public override void SetStaticDefaults()
		{
			BuffID.Sets.IsAFlaskBuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.ShardsOfAtheria().conductive = true;
		}
	}
}