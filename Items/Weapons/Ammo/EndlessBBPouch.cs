using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles.Ammo;

namespace SagesMania.Items.Weapons.Ammo
{
	public class EndlessBBPouch : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Endless BB Pouch");
		}

		public override void SetDefaults()
		{
			item.damage = 4;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.knockBack = 1.5f;
			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Green;
			item.shoot = ModContent.ProjectileType<BBProjectile>();
			item.shootSpeed = 16f;
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BB>(), 4000);
			recipe.AddTile(TileID.CrystalBall);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}