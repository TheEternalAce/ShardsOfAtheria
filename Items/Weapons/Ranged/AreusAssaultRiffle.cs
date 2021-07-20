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
	public class AreusAssaultRiffle : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Ugh!'");
		}

		public override void SetDefaults() 
		{
			item.damage = 96;
			item.ranged = true;
			item.noMelee = true;
			item.width = 44;
			item.height = 26;
			item.scale = .85f;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.crit = 5;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ItemID.PurificationPowder;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			areusResourceCost = 1;
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
			return new Vector2(-6, 0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
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