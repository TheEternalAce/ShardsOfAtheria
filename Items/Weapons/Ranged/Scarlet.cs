using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class Scarlet : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 60;
			Item.height = 22;

			Item.damage = 250;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 3.75f;
			Item.crit = 8;

			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.noMelee = true;

			Item.shootSpeed = 13f;
			Item.rare = ItemRarityID.Green;
			Item.value = 42500;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, -2);
		}

		public override void HoldItem(Player player)
		{
			player.scope = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<HuntingRifle>())
				.AddIngredient(ModContent.ItemType<BrokenHeroGun>())
				.AddIngredient(ItemID.SniperRifle)
				.AddIngredient(ItemID.FragmentVortex, 20)
				.AddIngredient(ItemID.LunarBar, 10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override bool CanConsumeAmmo(Item item, Player player)
		{
			return Main.rand.NextFloat() >= .66f;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet || type == ModContent.ProjectileType<BBProj>())
			{
				type = ProjectileID.MoonlordBullet;
			}
		}
	}
}