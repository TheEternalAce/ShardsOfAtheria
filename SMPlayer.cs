using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania
{
	public class SMPlayer : ModPlayer
	{
		public bool livingMetalArmorPrevious;
		public bool livingMetalArmor;
		public bool hideLivingMetalArmor;
		public bool forceLivingMetalArmor;
		public bool MEGASystemOnline;
		public bool nullified;
		public bool areusBatteryElectrify;
		public bool BBBottle;
		public bool PhantomBulletBottle;
		public bool Co2Cartridge;

		public override void ResetEffects()
		{
			nullified = false;
			livingMetalArmorPrevious = livingMetalArmor;
			livingMetalArmor = hideLivingMetalArmor = forceLivingMetalArmor = MEGASystemOnline = false;
			areusBatteryElectrify = false;
			BBBottle = false;
			PhantomBulletBottle = false;
			Co2Cartridge = false;
		}
		public override void UpdateVanityAccessories()
		{
			for (int n = 13; n < 18 + player.extraAccessorySlots; n++)
			{
				Item item = player.armor[n];
				if (item.type == ModContent.ItemType<Items.Accessories.LivingMetal>())
				{
					hideLivingMetalArmor = false;
					forceLivingMetalArmor = true;
				}
			}
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

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
		{
			// Make sure this condition is the same as the condition in the Buff to remove itself. We do this here instead of in ModItem.UpdateAccessory in case we want future upgraded items to set blockyAccessory
			if (livingMetalArmor)
			{
				player.AddBuff(ModContent.BuffType<Buffs.OnlineMEGASystem>(), 60, true);
			}
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

        public override void FrameEffects()
		{
			if ((livingMetalArmor || forceLivingMetalArmor) && !hideLivingMetalArmor)
			{
				player.legs = mod.GetEquipSlot("LivingMetalLeg", EquipType.Legs);
				player.body = mod.GetEquipSlot("LivingMetalBody", EquipType.Body);
				player.head = mod.GetEquipSlot("LivingMetalHead", EquipType.Head);
			}
			if (nullified)
			{
				Nullify();
			}
		}

		private void Nullify()
		{
			player.ResetEffects();
			player.head = -1;
			player.body = -1;
			player.legs = -1;
			player.handon = -1;
			player.handoff = -1;
			player.back = -1;
			player.front = -1;
			player.shoe = -1;
			player.waist = -1;
			player.shield = -1;
			player.neck = -1;
			player.face = -1;
			player.balloon = -1;
		}

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
			if (livingMetalArmor) Main.PlaySound(SoundID.Mech, player.position, 13);
		}
    }
}