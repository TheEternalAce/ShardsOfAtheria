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
				.AddIngredient(ItemID.FrozenTurtleShell)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.ShardsOfAtheria().diamanodShield = true;
			player.noKnockback = true;
			if (player.statLife <= player.statLifeMax * 0.5)
			{
				player.AddBuff(62, 5);
			}
		}
	}
}