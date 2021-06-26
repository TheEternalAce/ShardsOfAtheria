using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class PhantomRose : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots an extra Phantom Bullet\n" +
				"48% chance to not consume ammo\n" +
				"[c/960096:''Scarlet's younger sister'']");
		}

		public override void SetDefaults()
		{
			item.damage = 425;
			item.ranged = true;
			item.noMelee = true;
			item.width = 42;
			item.height = 30;
			item.scale = .85f;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item41;
			item.autoReuse = false;
			item.crit = 20;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 13f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 2);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<HeroGun>());
			recipe.AddIngredient(ItemID.PhoenixBlaster);
			recipe.AddIngredient(ItemID.FragmentVortex, 20);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .48f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<PhantomBullet>(), damage, knockBack, player.whoAmI);
			return true;
		}
    }
}