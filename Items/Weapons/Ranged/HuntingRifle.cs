using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Ammo;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class HuntingRifle : ModItem
	{
		public override void SetDefaults() 
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 58;
			Item.height = 26;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3.75f;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.crit = 6;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 13f;
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
				.AddRecipeGroup(SoARecipes.Copper, 4)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}