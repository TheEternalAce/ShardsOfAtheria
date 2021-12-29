using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Minions;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class SuperSapphireCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("15% chance to dodge damage\n" +
				"+5 aditional minion slots\n" +
				"'Calls a sapphire spirit to fight along side you'");
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
				.AddIngredient(ModContent.ItemType<GreaterSapphireCore>())
				.AddIngredient(ItemID.FragmentStardust, 5)
				.AddIngredient(ItemID.FragmentVortex, 5)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.AddBuff(ModContent.BuffType<SapphireSpirit>(), 2);
			player.GetModPlayer<SMPlayer>().superSapphireCore = true;
			player.GetModPlayer<SMPlayer>().superSapphireCore = true;
			player.maxMinions += 5;
		}
    }
}