using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using Terraria;
using SagesMania.Projectiles;
using Microsoft.Xna.Framework;
using SagesMania.Buffs;

namespace SagesMania.Items.Weapons.Magic
{
	public class AreusStaff : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("''It's magic, so it won't shock you. I think.''");
		}

		public override void SetDefaults() 
		{
			item.damage = 130;
			item.magic = true;
			item.noMelee = true;
			Item.staff[item.type] = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.crit = 16;
			item.value = Item.sellPrice(gold: 20);
			item.shoot = ModContent.ProjectileType<ElectricBolt>();
			item.shootSpeed = 16f;
			item.mana = 6;
			areusResourceCost = 1;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 10);
			recipe.AddIngredient(ItemID.FragmentVortex, 7);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.HasBuff(ModContent.BuffType<Overdrive>()))
				type = ModContent.ProjectileType<ElectricBlast>();
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			var areusDamagePlayer = player.GetModPlayer<SMPlayer>();

			if (areusDamagePlayer.areusResourceCurrent >= areusResourceCost && player.statMana >= item.mana)
			{
				areusDamagePlayer.areusResourceCurrent -= areusResourceCost;
				return true;
			}
			return false;
		}
	}
}