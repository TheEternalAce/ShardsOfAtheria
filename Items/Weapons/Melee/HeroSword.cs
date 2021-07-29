using SagesMania.Projectiles;
using SagesMania.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Melee
{
	public class HeroSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("The sword of a long forgotten hero");
		}

		public override void SetDefaults() 
		{
			item.damage = 80;
			item.melee = true;
			item.width = 62;
			item.height = 62;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 0;
			item.value = Item.sellPrice(gold: 80);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.crit = 96;
			item.shoot = ModContent.ProjectileType<HeroBlade>();
			item.shootSpeed = 10;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 2);
			recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.NightsEdge);
			recipe.AddIngredient(ItemID.Excalibur);
			recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
			recipe.SetResult(ItemID.TerraBlade);
			recipe.AddRecipe();
		}
	}
}