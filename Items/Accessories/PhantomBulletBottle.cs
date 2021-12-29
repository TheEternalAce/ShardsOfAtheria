using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using ShardsOfAtheria.Items.Weapons.Ammo;

namespace ShardsOfAtheria.Items.Accessories
{
	public class PhantomBulletBottle: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantom Bullet Bottle");
			Tooltip.SetDefault("48% chance to not consume ammo\n" +
				"Fires an extra Phantom Bullet");
		}

		public override void SetDefaults()
		{
			Item.width = 15;
			Item.height = 22;
			Item.value = Item.sellPrice(gold: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().PhantomBulletBottle = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BBBottle>())
				.AddIngredient(ItemID.FragmentVortex, 5)
				.AddIngredient(ItemID.LunarBar, 5)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}