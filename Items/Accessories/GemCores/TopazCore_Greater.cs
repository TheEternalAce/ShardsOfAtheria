using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class TopazCore_Greater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greater Topaz Core");
			Tooltip.SetDefault("Increases max life by 40\n" +
				"Honey and Regeneration buffs");
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
				.AddIngredient(ModContent.ItemType<TopazCore>())
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddIngredient(ItemID.RegenerationPotion, 15)
				.AddIngredient(ItemID.BottledHoney, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statLifeMax2 += 40;
			player.AddBuff(BuffID.Regeneration, 2);
			player.AddBuff(BuffID.Honey, 2);
		}
	}
}