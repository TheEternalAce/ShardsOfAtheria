using Microsoft.Xna.Framework;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class HuntingRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 58;
			Item.height = 26;

			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 3.75f;
			Item.crit = 6;

			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.noMelee = true;

			Item.shootSpeed = 13f;
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(0, 0, 25);
			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -2);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup(RecipeGroupID.Wood, 15)
				.AddRecipeGroup(ShardsRecipes.Copper, 4)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}