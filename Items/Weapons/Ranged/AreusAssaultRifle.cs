using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using SagesMania.Projectiles;
using SagesMania.Buffs;
using System.Collections.Generic;
using Terraria.Audio;

namespace SagesMania.Items.Weapons.Ranged
{
	public class AreusAssaultRifle : AreusWeapon
	{
		public override bool CloneNewInstances => true;
		private int fireMode;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Ugh!'\n" +
				"<right> to switch between 3 modes: Semi-auto, Burst Fire and Full-auto\n" +
				"66% chance to not consume ammo");
		}

		public override void SetDefaults() 
		{
			item.damage = 96;
			item.ranged = true;
			item.noMelee = true;
			item.width = 44;
			item.height = 26;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item11;
			item.autoReuse = false;
			item.crit = 5;
			item.rare = ItemRarityID.Cyan;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ItemID.PurificationPowder;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;

			if (!Config.areusWeaponsCostMana)
				areusResourceCost = 1;
			else item.mana = 4;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 15);
			recipe.AddIngredient(ItemID.FragmentVortex, 10);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (fireMode == 1)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
				speedX = perturbedSpeed.X;
				speedY = perturbedSpeed.Y;
			}
			if (fireMode == 2)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
				speedX = perturbedSpeed.X;
				speedY = perturbedSpeed.Y;
			}
			if (player.HasBuff(ModContent.BuffType<Overdrive>()))
				type = ModContent.ProjectileType<ElectricBlast>();
			if (type == ProjectileID.Bullet)
				type = ModContent.ProjectileType<ElectricBeam>();
			return true;
		}

		public override bool ConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (useAnimation - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			if (fireMode == 1)
				return !(player.itemAnimation < item.useAnimation - 2) || Main.rand.NextFloat() >= .66f;
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
				item.useTime = 6;
				item.useAnimation = 6;
				item.reuseDelay = 20;
				item.autoReuse = false;
				item.shoot = ItemID.None;
				item.UseSound = new LegacySoundStyle(SoundID.Unlock, 0);
				if (!Config.areusWeaponsCostMana)
					areusResourceCost = 0;
				else item.mana = 0;
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
					item.shoot = ItemID.PurificationPowder;
					item.UseSound = SoundID.Item11;
					item.useTime = 6;
					item.useAnimation = 6;
					item.reuseDelay = default;
					item.autoReuse = false;
					if (!Config.areusWeaponsCostMana)
						areusResourceCost = 1;
					else item.mana = 4;
				}
				else if (fireMode == 1)
				{
					item.shoot = ItemID.PurificationPowder;
					item.UseSound = SoundID.Item31;
					item.useTime = 4;
					item.useAnimation = 12;
					item.reuseDelay = 18;
					item.autoReuse = true;
					if (!Config.areusWeaponsCostMana)
						areusResourceCost = 4;
					else item.mana = 16;
				}
				else if (fireMode == 2)
				{
					item.shoot = ItemID.PurificationPowder;
					item.UseSound = SoundID.Item11;
					item.useTime = 6;
					item.useAnimation = 6;
					item.reuseDelay = default;
					item.autoReuse = true;
					if (!Config.areusWeaponsCostMana)
						areusResourceCost = 1;
					else item.mana = 4;
				}
			}
			return base.CanUseItem(player);
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.LocalPlayer;
			if (areusResourceCost > 0)
				tooltips.Add(new TooltipLine(mod, "Areus Resource Cost", $"Uses {areusResourceCost} areus charge"));
			if (fireMode == 0)
				tooltips.Add(new TooltipLine(mod, "Fire mode", "Semi-auto"));
			if (fireMode == 1)
				tooltips.Add(new TooltipLine(mod, "Fire mode", "Burst fire"));
			if (fireMode == 2)
				tooltips.Add(new TooltipLine(mod, "Fire mode", "Full-auto"));
		}
    }
}