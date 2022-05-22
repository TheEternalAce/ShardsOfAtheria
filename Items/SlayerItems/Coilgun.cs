using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ShardsOfAtheria.Items.Weapons.Ammo;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class Coilgun : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses Rods as ammo\n" +
				"Tears through enemy armor\n" +
				"'Uses electro magnets to fire projectiles at insane velocities'\n" +
				"'Areus Railgun's older brother'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 150;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 44;
			Item.height = 26;
			Item.useTime = 48;
			Item.useAnimation = 48;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item38;
			Item.autoReuse = false;
			Item.crit = 5;
			Item.shoot = ItemID.PurificationPowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;
			Item.rare = ModContent.RarityType<SlayerRarity>();
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			velocity *= 2.5f;
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}

        public override void HoldItem(Player player)
        {
			player.GetArmorPenetration(DamageClass.Generic) = 20;
        }
	}
}