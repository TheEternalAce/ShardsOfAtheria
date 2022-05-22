using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class AreusAssaultRifle : AreusWeapon
	{
		private int fireMode;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Ugh!'\n" +
				"<right> to switch between 3 modes: Semi-auto, Burst Fire and Full-auto\n" +
				"66% chance to not consume ammo");
		}

		public override void SetDefaults() 
		{
			Item.damage = 96;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 44;
			Item.height = 26;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = false;
			Item.crit = 5;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.sellPrice(0,  25);
			Item.shoot = ItemID.PurificationPowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;

			if (ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
				Item.mana = 4;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 15)
				.AddIngredient(ItemID.FragmentVortex, 10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return true;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (fireMode == 1)
			{
				velocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));
			}
			if (fireMode == 2)
			{
				velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			}
			if (type == ProjectileID.Bullet)
				type = ModContent.ProjectileType<ElectricBeam>();
		}

        public override bool CanConsumeAmmo(Player player)
		{
			if (fireMode == 1)
				return !(player.itemAnimation < Item.useAnimation - 2) || Main.rand.NextFloat() >= .66f;
			return Main.rand.NextFloat() >= .66f;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 6;
				Item.useAnimation = 6;
				Item.reuseDelay = 20;
				Item.autoReuse = false;
				Item.shoot = ItemID.None;
				Item.UseSound = new LegacySoundStyle(SoundID.Unlock, 0);
				if (!ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
					chargeCost = 0;
				else Item.mana = 0;
				fireMode += 1;
				if (fireMode == 3)
					fireMode = 0;
				if (fireMode == 0)
					CombatText.NewText(player.Hitbox, Color.White, "Semi-auto");
				if (fireMode == 1)
					CombatText.NewText(player.Hitbox, Color.White, "Burst Fire");
				if (fireMode == 2)
					CombatText.NewText(player.Hitbox, Color.White, "Full-auto");
			}
			else
			{
				if (fireMode == 0)
				{
					Item.shoot = ItemID.PurificationPowder;
					Item.UseSound = SoundID.Item11;
					Item.useTime = 6;
					Item.useAnimation = 6;
					Item.reuseDelay = default;
					Item.autoReuse = false;
					if (ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
						Item.mana = 4;
					else chargeCost = 1;
				}
				else if (fireMode == 1)
				{
					Item.shoot = ItemID.PurificationPowder;
					Item.UseSound = SoundID.Item31;
					Item.useTime = 4;
					Item.useAnimation = 12;
					Item.reuseDelay = 18;
					Item.autoReuse = true;
					if (ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
						Item.mana = 12;
					else chargeCost = 4;
				}
				else if (fireMode == 2)
				{
					Item.shoot = ItemID.PurificationPowder;
					Item.UseSound = SoundID.Item11;
					Item.useTime = 6;
					Item.useAnimation = 6;
					Item.reuseDelay = default;
					Item.autoReuse = true;
					if (ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
						Item.mana = 4;
					else chargeCost = 1;
				}
			}
			return base.CanUseItem(player);
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (fireMode == 0)
				tooltips.Add(new TooltipLine(Mod, "Fire mode", "Semi-auto"));
			if (fireMode == 1)
				tooltips.Add(new TooltipLine(Mod, "Fire mode", "Burst fire"));
			if (fireMode == 2)
				tooltips.Add(new TooltipLine(Mod, "Fire mode", "Full-auto"));
			base.ModifyTooltips(tooltips);
		}
    }
}