using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles.Ammo;
using System.Collections.Generic;

namespace SagesMania.Items.Weapons.Ammo
{
	public class BB : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("BB");
		}

		public override void SetDefaults()
		{
			item.damage = 4;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 1.5f;
			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Green;
			item.shoot = ModContent.ProjectileType<BBProjectile>();
			item.shootSpeed = 16f;
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SM:CopperBars");
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "Special Item", "[c/FF6400:Special Item]"));
		}
	}
}