using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;

namespace SagesMania.Items.Weapons.Ammo
{
	public class EndlessInfectionBulletPouch : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			item.damage = 26;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.knockBack = 1.5f;
			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Green;
			item.shoot = ModContent.ProjectileType<InfectionBulletProjectile>();
			item.shootSpeed = 16f;
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<InfectionBullet>(), 4000);
			recipe.AddTile(TileID.CrystalBall);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}