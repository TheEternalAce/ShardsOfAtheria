using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using SagesMania.Items.Weapons.Ammo;
using Microsoft.Xna.Framework;
using SagesMania.Projectiles;

namespace SagesMania.Items.SlayerItems
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
			item.damage = 150;
			item.magic = true;
			item.noMelee = true;
			item.width = 40;
			item.height = 22;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4f;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.crit = 8;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ItemID.PurificationPowder;
			item.shootSpeed = 20f;
			item.useAmmo = AmmoID.Flare;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			type = ModContent.ProjectileType<SolarFlare>();
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(4, 0);
		}
    }
}