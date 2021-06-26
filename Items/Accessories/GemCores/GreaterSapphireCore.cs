using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;
using SagesMania.Projectiles.Minions;

namespace SagesMania.Items.Accessories.GemCores
{
	public class GreaterSapphireCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("10% chance to dodge damage\n" +
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
			recipe.AddIngredient(ModContent.ItemType<SapphireCore>());
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.AddBuff(ModContent.BuffType<SapphireSpirit>(), 2);
			player.GetModPlayer<SMPlayer>().sapphireCore = true;
			player.GetModPlayer<SMPlayer>().greaterSapphireCore = true;
		}
    }
}