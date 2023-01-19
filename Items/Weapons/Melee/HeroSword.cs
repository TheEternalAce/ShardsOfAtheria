using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class HeroSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
			WeaponElements.Metal.Add(Type);
		}

		public override void SetDefaults()
		{
			Item.width = 62;
			Item.height = 62;

			Item.damage = 160;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 6;
			Item.crit = 6;

			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;

			Item.shoot = ModContent.ProjectileType<Projectiles.Weapon.Melee.HeroSword>();
			Item.shootSpeed = 1;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 2, 50);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.BrokenHeroSword)
				.AddIngredient(ItemID.HallowedBar, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}