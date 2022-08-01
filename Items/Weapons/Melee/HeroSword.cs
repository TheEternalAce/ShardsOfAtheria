using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class HeroSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("'The sword of a long forgotten hero'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 62;
			Item.height = 62;

			Item.damage = 80;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 6;
			Item.crit = 6;

			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 10;

			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 2, 50);
			Item.shoot = ModContent.ProjectileType<HeroBlade>();
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