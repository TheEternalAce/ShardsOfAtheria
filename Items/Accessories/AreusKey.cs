using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
	public class AreusKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Unlocks your true potential\n" +
				"'Now, nothing but your own competence holds you back.'");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 22;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.Cyan;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().areusKey = true;
			player.GetDamage(DamageClass.Generic) += .5f;
			player.statLifeMax2 *= 2;
			player.moveSpeed += .5f;
			player.statDefense *= 2;
			player.statManaMax2 *= 2;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 3)
				.AddIngredient(ItemID.ShadowKey)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}