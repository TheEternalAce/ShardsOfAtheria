using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Players;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
    public class SapphireCore_Greater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greater Sapphire Core");
			Tooltip.SetDefault("10% chance to dodge damage\n" +
				"Thorns effect");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;

			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(0, 2, 25);
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