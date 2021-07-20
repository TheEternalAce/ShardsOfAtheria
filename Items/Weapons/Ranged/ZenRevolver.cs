using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Ranged
{
	public class ZenRevolver : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.ranged = true;
			item.noMelee = true;
			item.width = 48;
			item.height = 28;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item41;
			item.autoReuse = true;
			item.crit = 6;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 13f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FlintlockPistol);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 16);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 6);
        }
    }
}