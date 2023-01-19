using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class TopazCore_Greater : ModItem
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

			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(0, 2, 25);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<TopazCore>())
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddIngredient(ItemID.RegenerationPotion, 15)
				.AddIngredient(ItemID.BottledHoney, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SoAPlayer>().topazNecklace = !hideVisual;
			player.statLifeMax2 += 40;
			player.AddBuff(BuffID.Regeneration, 2);
			player.AddBuff(BuffID.Honey, 2);
		}
	}
}