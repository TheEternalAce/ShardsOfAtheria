using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Items.Weapons.Biochemical;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
	public class VirusNeedle : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = ModContent.GetInstance<BiochemicalDamage>();
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(copper: 10);
			Item.rare = ItemRarityID.Blue;
			Item.shoot = ModContent.ProjectileType<VirusNeedleProj>();
			Item.shootSpeed = 16f;
			Item.ammo = ModContent.ItemType<BacteriaNeedle>();
		}

		public override void AddRecipes()
        {
			CreateRecipe(5)
				.AddIngredient(ModContent.ItemType<EmptyNeedle>(), 5)
				.AddRecipeGroup(SoARecipes.EvilMaterial, 5)
				.AddIngredient(ModContent.ItemType<Virus>(), 50)
				.Register();
        }
    }
}