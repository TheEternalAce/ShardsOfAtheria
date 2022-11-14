using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class Zenova : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 76;
			Item.height = 76;

			Item.damage = 263;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 3;
			Item.crit = 8;

			Item.useTime = 5;
			Item.useAnimation = 25;
			Item.reuseDelay = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.DD2_SkyDragonsFuryShot;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;

			Item.shootSpeed = 15;
			Item.value = Item.sellPrice(0, 4);
			Item.rare = ItemRarityID.Red;
			Item.shoot = ModContent.ProjectileType<ZenovaProjectile>();
			Item.ArmorPenetration = 37;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.WoodenSword)
				.AddIngredient(ItemID.BoneJavelin, 180)
				.AddIngredient(ItemID.BreakerBlade)
				.AddIngredient(ItemID.ChlorophyteSaber)
				.AddIngredient(ItemID.DayBreak)
				.AddIngredient(ModContent.ItemType<Satanlance>())
				.AddIngredient(ModContent.ItemType<AreusSword>())
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			SoundEngine.PlaySound(Item.UseSound);
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
		}
	}
}