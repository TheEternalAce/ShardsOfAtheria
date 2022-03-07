using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class Glock80 : ModItem
	{
		int bulletsLoaded;
		int reload;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glock 80");
			Tooltip.SetDefault("'Automatic shotgun pistol, because it's funny'");
		}

		public override void SetDefaults()
		{
			Item.damage = 1;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 42;
			Item.height = 30;
			Item.scale = .60f;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3.75f;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item41;
			Item.autoReuse = true;
			Item.crit = 0;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 13f;
			Item.useAmmo = AmmoID.Bullet;
			Item.expert = true;
			Item.value = Item.sellPrice(gold: 5);

			bulletsLoaded = 8;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}

        public override void HoldItem(Player player)
		{
			if (bulletsLoaded != 0 && reload == 0)
			{
				Item.UseSound = SoundID.Item41;
			}
			else if (bulletsLoaded == 0)
			{
				Item.autoReuse = false;
				Item.UseSound = new LegacySoundStyle(SoundID.Unlock, 0);
			}

			if (reload > 4)
			{
				reload = 0;
				bulletsLoaded = 8;
			}
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (bulletsLoaded != 0)
            {
				bulletsLoaded--;
				Item.autoReuse = true;
			}
			if (bulletsLoaded == 0)
			{
				reload++;
			}

			if (Item.UseSound == SoundID.Item41)
			{
				const int NumProjectiles = 8; // The number of projectiles that this gun will shoot.

				for (int i = 0; i < NumProjectiles; i++)
				{
					// Rotate the velocity randomly by 30 degrees at max.
					Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

					// Decrease velocity randomly for nicer visuals.
					newVelocity *= 1f - Main.rand.NextFloat(0.3f);

					// Create a Projectile.
					Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
				}
			}
			return false; // Return false because we don't want tModLoader to shoot projectile
		}
	}
}