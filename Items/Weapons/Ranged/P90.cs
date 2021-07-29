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
	public class P90 : ModItem
	{
		public override bool CloneNewInstances => true;
		private bool fullAuto;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("<right> to switch between 2 modes: Semi and Full-auto");
		}

		public override void SetDefaults() 
		{
			item.damage = 30;
			item.ranged = true;
			item.noMelee = true;
			item.width = 82;
			item.height = 36;
			item.useTime = 6;
			item.useAnimation = 6;
			item.scale = .7f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = .1f;
			item.UseSound = SoundID.Item40;
			item.crit = 5;
			item.rare = ItemRarityID.Pink;
			item.value = Item.sellPrice(gold: 5);
			item.shoot = ItemID.PurificationPowder;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
			recipe.AddIngredient(ItemID.GoldBar, 5);
			recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, -1);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.Bullet && Main.rand.Next(2) == 0)
            {
					type = ProjectileID.ChlorophyteBullet;
            }
				
            if (fullAuto)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
				speedX = perturbedSpeed.X;
				speedY = perturbedSpeed.Y;
			}
			return true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useTime = 20;
				item.useAnimation = 20;
				item.autoReuse = false;
				item.shoot = ItemID.None;
				item.UseSound = new LegacySoundStyle(SoundID.Unlock, 0);
				if (fullAuto)
					fullAuto = false;
				else fullAuto = true;
				if (!fullAuto)
					CombatText.NewText(player.Hitbox, Color.White, "Semi-auto");
				if (fullAuto)
					CombatText.NewText(player.Hitbox, Color.White, "Full-auto");
			}
			else
			{
				if (!fullAuto)
				{
					item.shoot = ItemID.PurificationPowder;
					item.useTime = 6;
					item.useAnimation = 6;
					item.UseSound = SoundID.Item40;
					item.autoReuse = false;
				}
				else if (fullAuto)
				{
					item.shoot = ItemID.PurificationPowder;
					item.useTime = 6;
					item.useAnimation = 6;
					item.UseSound = SoundID.Item40;
					item.autoReuse = true;
				}
			}
			return base.CanUseItem(player);
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (!fullAuto)
				tooltips.Add(new TooltipLine(mod, "Fire mode", "Semi-auto"));
			if (fullAuto)
				tooltips.Add(new TooltipLine(mod, "Fire mode", "Full-auto"));
		}
    }
}