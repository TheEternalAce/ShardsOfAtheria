using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Weapons.Ammo;

namespace SagesMania.Items.Accessories
{
	public class CO2Cartridge: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("CO2 Cartridge");
			Tooltip.SetDefault("Converts bullets into High Velocity Bullets");
		}

		public override void SetDefaults()
		{
			item.width = 15;
			item.height = 22;
			item.value = Item.buyPrice(gold: 5);
			item.value = Item.sellPrice(silver: 75);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SMPlayer p = player.GetModPlayer<SMPlayer>();
			p.Co2Cartridge = true;
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