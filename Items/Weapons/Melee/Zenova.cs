using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles;
using ShardsOfAtheria.Projectiles.Weapon;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class Zenova : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Ignores a moderate ammount of defense\n" +
				"'Zenith's older sister'\n" +
				"'RANDOM BULLS**T GO!'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 263;
			Item.DamageType = DamageClass.Melee;
			Item.width = 76;
			Item.height = 76;
			Item.useTime = 1;
			Item.useAnimation = 1;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(gold: 25);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.crit = 16;
			Item.shoot = ModContent.ProjectileType<ZenovaProjectile>();
			Item.shootSpeed = 15;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ItemID.WoodenSword)
				.AddIngredient(ItemID.BoneJavelin, 180)
				.AddIngredient(ModContent.ItemType<Cataracnia>())
				.AddIngredient(ModContent.ItemType<OversizedWormsTooth>())
				.AddIngredient(ItemID.BreakerBlade)
				.AddIngredient(ItemID.ChlorophyteSaber)
				.AddIngredient(ItemID.DayBreak)
				.AddIngredient(ModContent.ItemType<LostNail>())
				.AddIngredient(ModContent.ItemType<Satanlance>())
				.AddIngredient(ModContent.ItemType<BlackAreusSword>())
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

        // How can I choose between several projectiles randomly?
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod Projectile.
			type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<ZenovaDaybreak>(), ModContent.ProjectileType<ZenovaBoneJavelin>(),
				ModContent.ProjectileType<ZenovaProjectile>(), ModContent.ProjectileType<ZenovaBlackAreusSword>(),
				ModContent.ProjectileType<ZenovaBreakerBlade>(), ModContent.ProjectileType<ZenovaCataracnia>(),
				ModContent.ProjectileType<ZenovaChlorophyteSaber>(), ModContent.ProjectileType<ZenovaSatanlance>(),
				ModContent.ProjectileType<ZenovaWoodenSword>(), ModContent.ProjectileType<ZenovaWormsTooth>(),
				ModContent.ProjectileType<ElectricBlade>(), ModContent.ProjectileType<ZenovaLostNail>(),
				ModContent.ProjectileType<InfectionBlob>()});
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
		}

        public override void HoldItem(Player player)
        {
			player.armorPenetration = 37;
        }
    }
}