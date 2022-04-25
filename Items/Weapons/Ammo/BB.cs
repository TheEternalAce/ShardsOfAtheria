using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
	public class BB : SpecialItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("BB");
		}

		public override void SetDefaults()
		{
			Item.damage = 4;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(copper: 1);
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<BBProjectile>();
			Item.shootSpeed = 16f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			CreateRecipe(50)
				.AddRecipeGroup(SoARecipes.Copper)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}