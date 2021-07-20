using Microsoft.Xna.Framework;
using SagesMania.Items.Weapons.Ammo;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Ranged
{
	public class BBGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BB Gun");
			Tooltip.SetDefault("[c/960096:''One of my favorite treasures, take care of it'']");
		}

		public override void SetDefaults() 
		{
			item.damage = 10;
			item.ranged = true;
			item.noMelee = true;
			item.width = 50;
			item.height = 16;
			item.useTime = 40;
			item.useAnimation = 40;
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
			return new Vector2(-4, 2);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "Special Item", "[c/FF6400:Special Item]"));
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