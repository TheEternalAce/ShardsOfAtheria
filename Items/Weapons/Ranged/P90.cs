using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class P90 : ModItem
	{
		private bool fullAuto;

		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 20;

			Item.damage = 46;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = .1f;
			Item.crit = 5;

			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item40;
			Item.noMelee = true;

			Item.shootSpeed = 16f;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(0, 3, 25);
			Item.shoot = ItemID.PurificationPowder;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.ChlorophyteBar, 15)
				.AddIngredient(ItemID.GoldBar, 5)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-16, -1);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet && Main.rand.NextBool(2))
			{
				type = ProjectileID.ChlorophyteBullet;
			}

			if (fullAuto)
			{
				velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.autoReuse = false;
				Item.shoot = ItemID.None;
				Item.UseSound = SoundID.Unlock;
				if (fullAuto)
					fullAuto = false;
				else fullAuto = true;
				if (!fullAuto)
					CombatText.NewText(player.Hitbox, Color.White, Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode1"));
				if (fullAuto)
					CombatText.NewText(player.Hitbox, Color.White, Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode3"));
			}
			else
			{
				if (!fullAuto)
				{
					Item.shoot = ItemID.PurificationPowder;
					Item.useTime = 6;
					Item.useAnimation = 6;
					Item.UseSound = SoundID.Item40;
					Item.autoReuse = false;
				}
				else if (fullAuto)
				{
					Item.shoot = ItemID.PurificationPowder;
					Item.useTime = 6;
					Item.useAnimation = 6;
					Item.UseSound = SoundID.Item40;
					Item.autoReuse = true;
				}
			}
			return base.CanUseItem(player);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (!fullAuto)
				tooltips.Add(new TooltipLine(Mod, "Fire mode", Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode1")));
			if (fullAuto)
				tooltips.Add(new TooltipLine(Mod, "Fire mode", Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode3")));
		}
	}
}