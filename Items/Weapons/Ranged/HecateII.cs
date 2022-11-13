using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class HecateII : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 104;
			Item.height = 30;

			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.crit = 6;
			Item.knockBack = 8f;

			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item40;
			Item.noMelee = true;

			Item.shootSpeed = 16f;
			Item.rare = ItemRarityID.Expert;
			Item.expert = true;
			Item.value = Item.sellPrice(0, 2, 25);
			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = AmmoID.Bullet;
		}

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-16, -2);
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet || type == ModContent.ProjectileType<BBProj>())
			{
				type = ProjectileID.BulletHighVelocity;
			}
		}

        public override void HoldItem(Player player)
        {
			player.scope = true;
		}
	}
}