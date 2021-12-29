using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Ammo;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
	public class EndlessInfectionBulletPouch : ModItem
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
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<InfectionBulletProjectile>();
			Item.shootSpeed = 16f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<InfectionBullet>(), 4000)
				.AddTile(TileID.CrystalBall)
				.Register();
        }
    }
}