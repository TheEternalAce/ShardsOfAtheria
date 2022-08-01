using ShardsOfAtheria.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
    public class SapphireCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("10% chance to dodge attacks");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 1, 25);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<SapphireCore_Lesser>())
				.AddIngredient(ItemID.HellstoneBar, 5)
				.AddIngredient(ItemID.Bone, 20)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SoAPlayer>().sapphireCore = true;
        }
	}
}