using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class BBGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BB Gun");
			Tooltip.SetDefault("[c/960096:''One of my favorite treasures, take care of it'']\n" +
				"[c/FF6400:Special Item]");
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
			return new Vector2(-24, 4);
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 15);
			recipe.AddRecipeGroup("SM:CopperBars", 4);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}