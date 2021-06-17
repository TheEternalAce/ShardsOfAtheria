using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SagesMania.Items.AreusDamageClass;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class HeroGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The gun of a long forgotten hero");
		}

		public override void SetDefaults()
		{
			item.damage = 72;
			item.ranged = true;
			item.noMelee = true;
			item.width = 56;
			item.height = 24;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = false;
			item.crit = 20;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 13f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BrokenHeroGun>(), 2);
			recipe.AddIngredient(ItemID.Handgun);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .48f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<HeroBullet>(), damage, knockBack, player.whoAmI);
			return true;
		}
    }
}