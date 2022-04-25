using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using ShardsOfAtheria.Items.Weapons.Biochemical;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
	public class BacteriaNeedle : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			Item.damage = 7;
			Item.DamageType = ModContent.GetInstance<BiochemicalDamage>();
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(copper: 5);
			Item.rare = ItemRarityID.Blue;
			Item.shoot = ModContent.ProjectileType<BacteriaNeedleProj>();
			Item.shootSpeed = 16f;
			Item.ammo = Item.type;
		}

		public override void AddRecipes()
        {
			CreateRecipe(5)
				.AddIngredient(ModContent.ItemType<EmptyNeedle>(), 5)
				.AddIngredient(ModContent.ItemType<Bacteria>(), 50)
				.Register();
        }
    }
}