using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania
{
	public class SMPlayer : ModPlayer
	{
		public bool areusBatteryElectrify;
		public bool BBBottle;
		public bool PhantomBulletBottle;
		public bool Co2Cartridge;
		public bool naturalAreusRegen;

		public override void ResetEffects()
		{
			areusBatteryElectrify = false;
			BBBottle = false;
			PhantomBulletBottle = false;
			Co2Cartridge = false;
		}

		public override bool ConsumeAmmo(Item weapon, Item ammo)
		{
			if (BBBottle) {
				return Main.rand.NextFloat() >= .38f;
			}
			if (PhantomBulletBottle)
			{
				return Main.rand.NextFloat() >= .48f;
			}
			return true;
		}

		public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (PhantomBulletBottle)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<PhantomBullet>(), damage, knockBack, player.whoAmI);
			}
			if (BBBottle)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BBProjectile>(), damage, knockBack, player.whoAmI);
			}
			if (Co2Cartridge)
			{
				if (type == ProjectileID.Bullet)
				{
					type = ProjectileID.BulletHighVelocity;
				}
				return true;
			}
			return true;
		}
		
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
			if (areusBatteryElectrify)
            {
				target.AddBuff(BuffID.Electrified, 10*60);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
			if (areusBatteryElectrify)
			{
				target.AddBuff(BuffID.Electrified, 10 * 60);
			}
		}
	}
}