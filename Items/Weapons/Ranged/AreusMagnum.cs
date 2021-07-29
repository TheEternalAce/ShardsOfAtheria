using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using SagesMania.Projectiles;
using SagesMania.Buffs;

namespace SagesMania.Items.Weapons.Ranged
{
	public class AreusMagnum : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Headshots do not crit'");
		}

		public override void SetDefaults() 
		{
			item.damage = 37;
			item.ranged = true;
			item.noMelee = true;
			item.width = 44;
			item.height = 26;
			item.scale = .85f;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item41;
			item.autoReuse = false;
			item.crit = 5;
			item.rare = ItemRarityID.Cyan;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ItemID.PurificationPowder;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;

			if (!Config.areusWeaponsCostMana)
				areusResourceCost = 1;
			else item.mana = 5;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 5);
			recipe.AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 7);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-1, 0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.HasBuff(ModContent.BuffType<Overdrive>()))
				type = ModContent.ProjectileType<ElectricBlast>();
			if (type == ProjectileID.Bullet)
			{
				type = ModContent.ProjectileType<ElectricBeam>();
			}
			return true;
		}
    }
}