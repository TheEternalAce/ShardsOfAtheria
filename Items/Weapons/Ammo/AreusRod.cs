using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles.Ammo;
using SagesMania.Items.Placeable;

namespace SagesMania.Items.Weapons.Ammo
{
	public class AreusRod : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 1.5f;
			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Cyan;
			item.shoot = ModContent.ProjectileType<AreusRodProj>();
			item.shootSpeed = 16f;
			item.ammo = item.type;
		}

		public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>());
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
        }
    }
}