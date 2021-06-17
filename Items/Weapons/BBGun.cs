using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Weapons.Ammo;

namespace SagesMania.Items.Weapons
{
	public class BBGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BB Gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("[c/960096:One of my favorite treasures, take care of it]");
		}

		public override void SetDefaults() 
		{
			item.damage = 10;
			item.ranged = true;
			item.noMelee = true;
			item.width = 56;
			item.height = 18;
			item.useTime = 90;
			item.useAnimation = 90;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/BBGunShoot");
			item.autoReuse = true;
			item.crit = 6;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 13f;
			item.useAmmo = AmmoID.Bullet;
		}
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 2);
		}
		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 15);
			recipe.AddRecipeGroup("SM:CopperBars", 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}