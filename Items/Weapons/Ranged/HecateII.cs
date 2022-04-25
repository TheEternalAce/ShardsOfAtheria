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
			DisplayName.SetDefault("Hecate II");
			Tooltip.SetDefault("'She's a real monster.'\n" +
				"'Just like me.'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 93;
			Item.height = 20;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 8f;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item40;
			Item.crit = 6;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;
			Item.expert = true;
			Item.value = Item.sellPrice(0,  5);
		}

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-16, -2);
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet || type == ModContent.ProjectileType<BBProjectile>())
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