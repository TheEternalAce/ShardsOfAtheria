using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Melee.Zenova;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Zenova : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Ignores a moderate amount of defense\n" +
				"'Zenith's older sister'\n" +
				"'RANDOM BULLS**T GO!'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 76;
			Item.height = 76;

			Item.damage = 263;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 3;
			Item.crit = 8;

			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
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

        // How can I choose between several projectiles randomly?
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod Projectile.
			type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<ZenovaDaybreak>(), ModContent.ProjectileType<ZenovaBoneJavelin>(),
				ModContent.ProjectileType<ZenovaProjectile>(), ModContent.ProjectileType<ZenovaAreusSword>(),
				ModContent.ProjectileType<ZenovaBreakerBlade>(), ModContent.ProjectileType<ZenovaChlorophyteSaber>(),
				ModContent.ProjectileType<ZenovaSatanlance>(), ModContent.ProjectileType<ZenovaWoodenSword>(),
				ModContent.ProjectileType<ElectricBlade>(), ModContent.ProjectileType<ZenovaLostNail>()});
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
		}
    }
}