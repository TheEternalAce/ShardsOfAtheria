using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class HeroSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("The sword of a long forgotten hero");
		}

		public override void SetDefaults() 
		{
			Item.damage = 80;
			Item.DamageType = DamageClass.Melee;
			Item.width = 62;
			Item.height = 62;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.value = Item.sellPrice(0, 2, 50);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 6;
			Item.shoot = ModContent.ProjectileType<HeroBlade>();
			Item.shootSpeed = 10;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ItemID.BrokenHeroSword)
				.AddRecipeGroup(RecipeGroupID.IronBar, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}