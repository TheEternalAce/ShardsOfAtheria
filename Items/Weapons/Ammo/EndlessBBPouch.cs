using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
	public class EndlessBBPouch : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Endless BB Pouch");
		}

		public override void SetDefaults()
		{
			Item.damage = 4;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(silver: 60);
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<BBProjectile>();
			Item.shootSpeed = 16f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BB>(), 4000)
				.AddTile(TileID.CrystalBall)
				.Register();
        }
    }
}