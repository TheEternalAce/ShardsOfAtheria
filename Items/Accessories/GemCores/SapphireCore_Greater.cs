using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Minions;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	public class SapphireCore_Greater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greater Sapphire Core");
			Tooltip.SetDefault("10% chance to dodge damage\n" +
				"Thorns effect");
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
				.AddIngredient(ModContent.ItemType<SapphireCore>())
				.AddIngredient(ItemID.HallowedBar, 5)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<SoAPlayer>().sapphireCore = true;

			player.AddBuff(BuffID.Thorns, 2);
		}
    }
}