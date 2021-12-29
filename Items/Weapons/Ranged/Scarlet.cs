using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Projectiles.Ammo;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class Scarlet : SpecialItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots powerful Luminite Bullets\n" +
				"66% chance to not consume ammo\n" +
				"[c/960096:'Now we're talkin'!']");
		}

		public override void SetDefaults()
		{
			Item.damage = 500;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 56;
			Item.height = 18;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3.75f;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.crit = 20;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 13f;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 2);
		}

        public override void HoldItem(Player player)
        {
			player.scope = true;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BBGun>())
				.AddIngredient(ModContent.ItemType<BrokenHeroGun>())
				.AddIngredient(ItemID.SniperRifle)
				.AddIngredient(ItemID.FragmentVortex, 20)
				.AddIngredient(ItemID.LunarBar, 10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .66f;
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet || type == ModContent.ProjectileType<BBProjectile>())
			{
				type = ProjectileID.MoonlordBullet;
			}
		}
    }
}