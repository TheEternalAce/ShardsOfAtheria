using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class Zenova : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Ignores a moderate ammount of defense\n" +
				"'Zenith's older sister'\n" +
				"''RANDOM BULLS**T GO!''");
		}

		public override void SetDefaults() 
		{
			item.damage = 263;
			item.melee = true;
			item.width = 76;
			item.height = 76;
			item.useTime = 1;
			item.useAnimation = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = Item.sellPrice(gold: 25);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.crit = 16;
			item.shoot = ModContent.ProjectileType<ZenovaProjectile>();
			item.shootSpeed = 15;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenSword);
			recipe.AddIngredient(ItemID.BoneJavelin, 180);
			recipe.AddIngredient(ModContent.ItemType<Cataracnia>());
			recipe.AddIngredient(ModContent.ItemType<OversizedWormsTooth>());
			recipe.AddIngredient(ItemID.BreakerBlade);
			recipe.AddIngredient(ItemID.ChlorophyteSaber);
			recipe.AddIngredient(ItemID.DayBreak);
			recipe.AddIngredient(ModContent.ItemType<LostNail>());
			recipe.AddIngredient(ModContent.ItemType<Satanlance>());
			recipe.AddIngredient(ModContent.ItemType<BlackAreusSword>());
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		// How can I choose between several projectiles randomly?
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<ZenovaDaybreak>(), ModContent.ProjectileType<ZenovaBoneJavelin>(), 
				ModContent.ProjectileType<ZenovaProjectile>(), ModContent.ProjectileType<ZenovaBlackAreusSword>(),
				ModContent.ProjectileType<ZenovaBreakerBlade>(), ModContent.ProjectileType<ZenovaCataracnia>(),
				ModContent.ProjectileType<ZenovaChlorophyteSaber>(), ModContent.ProjectileType<ZenovaSatanlance>(),
				ModContent.ProjectileType<ZenovaWoodenSword>(), ModContent.ProjectileType<ZenovaWormsTooth>(),
				ModContent.ProjectileType<ElectricBlade>(), ModContent.ProjectileType<ZenovaLostNail>(),
				ModContent.ProjectileType<InfectionBlob>()});
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}

        public override void HoldItem(Player player)
        {
			player.armorPenetration = 37;
        }
    }
}