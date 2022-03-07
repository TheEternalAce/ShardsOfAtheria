using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Minions;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class SapphireCore_Super : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Sapphire Core");
			Tooltip.SetDefault("15% chance to dodge attacks\n" +
				"+5 aditional minion slots\n" +
				"Thorns effect\n" +
                "Inferno effect on hurt");
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
				.AddIngredient(ModContent.ItemType<SapphireCore_Greater>())
				.AddIngredient(ItemID.FragmentStardust, 5)
				.AddIngredient(ItemID.FragmentVortex, 5)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<SoAPlayer>().superSapphireCore = true;
			player.maxMinions += 5;
			player.AddBuff(BuffID.Thorns, 0);
		}
    }
}