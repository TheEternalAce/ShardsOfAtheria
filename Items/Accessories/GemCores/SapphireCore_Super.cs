using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Players;

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
                "Inferno effect when taking damage");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;

			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(0, 3);
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