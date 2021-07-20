using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using SagesMania.Projectiles;

namespace SagesMania.Items.SlayerItems
{
	public class WormTench : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Gross, absolutely vile..'");
		}

		public override void SetDefaults() 
		{
			item.damage = 40;
			item.magic = true;
			item.noMelee = true;
			item.width = 50;
			item.height = 26;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item17;
			item.autoReuse = false;
			item.crit = 5;
			item.value = Item.sellPrice(gold: 25);
			item.rare = ItemRarityID.Expert;
			item.shoot = ModContent.ProjectileType<VileShot>();
			item.shootSpeed = 16f;
			item.mana = 5;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(3, 1);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
				// 30 degree spread.
				// If you want to randomize the speed to stagger the projectiles
				// float scale = 1f - (Main.rand.NextFloat() * .3f);
				// perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; // return false because we don't want tmodloader to shoot projectile
		}
	}
}