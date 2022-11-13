using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class HeroGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 56;
			Item.height = 24;

			Item.damage = 72;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 3.75f;
			Item.crit = 6;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item41;
			Item.autoReuse = true;
			Item.noMelee = true;

			Item.shootSpeed = 13f;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(0, 3, 25);
			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BrokenHeroGun>())
				.AddIngredient(ItemID.Handgun)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override bool CanConsumeAmmo(Item item, Player player)
		{
			return Main.rand.NextFloat() >= .48f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<HeroBullet>(), damage, knockback, player.whoAmI);
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
	}
}