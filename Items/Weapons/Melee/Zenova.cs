using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using ShardsOfAtheria.Projectiles.Weapon.Melee.Zenova;
using Terraria;
using Terraria.Audio;
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
			Item.shoot = ModContent.ProjectileType<ZenovaProj>();
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
			SoundEngine.PlaySound(Item.UseSound);
            switch (Main.rand.Next(11))
            {
				case 0:
					type = ModContent.ProjectileType<ZenovaDaybreak>();
					break;
				case 1:
					type = ModContent.ProjectileType<ZenovaBoneJavelin>();
					break;
				case 2:
					type = ModContent.ProjectileType<ZenovaProj>();
					break;
				case 3:
					type = ModContent.ProjectileType<ZenovaAreusSword>();
					break;
				case 4:
					type = ModContent.ProjectileType<ZenovaBreakerBlade>();
					break;
				case 5:
					type = ModContent.ProjectileType<ZenovaChlorophyteSaber>();
					break;
				case 6:
					type = ModContent.ProjectileType<ZenovaSatanlance>();
					break;
				case 7:
					type = ModContent.ProjectileType<ZenovaWoodenSword>();
					break;
				case 8:
					type = ModContent.ProjectileType<ElectricBlade>();
					break;
				case 9:
					type = ModContent.ProjectileType<ZenovaLostNail>();
					break;
            }
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
		}
    }
}