using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
	public class BB : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("BB");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 4;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(copper: 1);
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<BBProjectile>();
			Item.shootSpeed = 4f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			CreateRecipe(50)
				.AddRecipeGroup(ShardsRecipes.Copper)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}