using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Weapons.Ammo;

namespace SagesMania.Items.Accessories
{
	public class BBBottle: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BB Bottle");
			Tooltip.SetDefault("5% chance to not consume ammo\n" +
				"Fires an extra BB");
		}

		public override void SetDefaults()
		{
			item.width = 15;
			item.height = 22;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().BBBottle = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BB>(), 200);
			recipe.AddIngredient(ItemID.Bottle);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}