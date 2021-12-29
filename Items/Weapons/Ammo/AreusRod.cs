using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Items.Placeable;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
	public class AreusRod : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Cyan;
			Item.shoot = ModContent.ProjectileType<AreusRodProj>();
			Item.shootSpeed = 16f;
			Item.ammo = Item.type;
		}

		public override void AddRecipes()
        {
			CreateRecipe(100)
				.AddIngredient(ModContent.ItemType<AreusBarItem>())
				.AddTile(TileID.WorkBenches)
				.Register();
        }
    }
}