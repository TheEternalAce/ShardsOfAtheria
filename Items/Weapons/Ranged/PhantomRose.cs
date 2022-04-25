using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class PhantomRose : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots an extra Phantom Bullet\n" +
				"48% chance to not consume ammo");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");
			line = new TooltipLine(Mod, "PhantomRose", "'Scarlet's younger sister'")
			{
				OverrideColor = Color.Red
			};
			tooltips.Add(line);
		}

		public override void SetDefaults()
		{
			Item.damage = 425;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 38;
			Item.height = 24;
			Item.scale = .85f;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3.75f;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item41;
			Item.autoReuse = false;
			Item.crit = 8;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 13f;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(6, -2);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<HeroGun>())
				.AddIngredient(ItemID.PhoenixBlaster)
				.AddIngredient(ItemID.FragmentVortex, 20)
				.AddIngredient(ItemID.LunarBar, 10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .48f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PhantomBullet>(), damage, knockback, player.whoAmI);
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}