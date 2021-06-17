using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using SagesMania.Items.Weapons.Ammo;

namespace SagesMania.Items.Accessories
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
			item.width = 15;
			item.height = 22;
			item.value = Item.sellPrice(gold: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().PhantomBulletBottle = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BBBottle>());
			recipe.AddIngredient(ItemID.FragmentVortex, 5);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}