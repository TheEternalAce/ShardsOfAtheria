using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using ShardsOfAtheria.Projectiles;
using ShardsOfAtheria.Buffs;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
	public class AreusPistol : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'100% safety guaranteed(?)'");
		}

		public override void SetDefaults() 
		{
			Item.damage = 76;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.width = 32;
			Item.height = 16;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3.75f;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = false;
			Item.crit = 16;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.sellPrice(gold: 25);
			Item.shoot = ModContent.ProjectileType<ElectricBolt>();
			Item.shootSpeed = 16f;

			if (!Config.areusWeaponsCostMana)
				areusResourceCost = 1;
			else Item.mana = 8;
		}

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(3, 4);
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 10)
				.AddIngredient(ItemID.SoulofSight, 7)
				.AddTile(ModContent.TileType<AreusForge>())
				.Register();
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet)
				type = ModContent.ProjectileType<ElectricBeam>();
		}
    }
}