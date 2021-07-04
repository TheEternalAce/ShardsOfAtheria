using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;
using SagesMania.Projectiles.Minions;

namespace SagesMania.Items.Accessories.GemCores
{
	public class SuperSapphireCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("15% chance to dodge damage\n" +
				"+5 aditional minion slots\n" +
				"Spore Sack effect\n" +
				"'Calls a sapphire spirit to fight along side you'");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GreaterSapphireCore>());
			recipe.AddIngredient(ItemID.FragmentStardust, 5);
			recipe.AddIngredient(ItemID.FragmentVortex, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.AddBuff(ModContent.BuffType<SapphireSpirit>(), 2);
			player.sporeSac = true;
			player.GetModPlayer<SMPlayer>().superSapphireCore = true;
			player.GetModPlayer<SMPlayer>().superSapphireCore = true;
			player.maxMinions += 5;
		}
    }
}