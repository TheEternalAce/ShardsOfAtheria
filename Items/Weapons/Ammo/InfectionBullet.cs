using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;
using SagesMania.Items.Placeable;

namespace SagesMania.Items.Weapons.Ammo
{
	public class InfectionBullet : ModItem
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
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 1.5f;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.shoot = ModContent.ProjectileType<InfectionBulletProjectile>();
			item.shootSpeed = 16f;
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MusketBall, 70);
			recipe.AddIngredient(ModContent.ItemType<CrystalInfection>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 70);
			recipe.AddRecipe();
        }
    }
}