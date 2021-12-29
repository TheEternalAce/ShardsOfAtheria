using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ShardsOfAtheria.Items.Weapons.Ammo;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class SolarStorm : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a storm of Solar Flares\n" +
				"'Harness energy from the sun to vaporize your foes!'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 150;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.width = 40;
			Item.height = 22;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4f;
			Item.rare = ItemRarityID.Expert;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.crit = 8;
			Item.value = Item.sellPrice(gold: 25);
			Item.shoot = ItemID.PurificationPowder;
			Item.shootSpeed = 20f;
			Item.useAmmo = AmmoID.Flare;
		}

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(4, 0);
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			if (type == ProjectileID.Flare)
				type = ModContent.ProjectileType<SolarFlare>();
        }
    }
}