using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class AreusMagnum : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Headshots do not crit'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 37;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 44;
			Item.height = 26;
			Item.scale = .85f;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item41;
			Item.autoReuse = false;
			Item.crit = 5;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.sellPrice(0,  25);
			Item.shoot = ItemID.PurificationPowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;

			if (ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
				Item.mana = 5;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 5)
				.AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 7)
				.AddIngredient(ItemID.HellstoneBar, 10)
				.AddTile(TileID.Hellforge)
				.Register();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-1, 0);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet)
				type = ModContent.ProjectileType<ElectricBeam>();
		}
	}
}