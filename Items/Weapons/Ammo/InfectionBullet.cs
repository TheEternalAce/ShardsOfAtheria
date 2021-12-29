using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
	public class InfectionBullet : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			Item.damage = 26;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<InfectionBulletProjectile>();
			Item.shootSpeed = 16f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			CreateRecipe(70)
				.AddIngredient(ItemID.MusketBall, 70)
				.AddIngredient(ModContent.ItemType<CrystalInfection>())
				.AddTile(TileID.Anvils)
				.Register();
        }
    }
}