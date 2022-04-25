using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
	public class AreusTrimmedTome : AreusWeapon
	{
		public override void SetDefaults() 
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3.75f;
			Item.UseSound = SoundID.Item43;
			Item.crit = 16;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.sellPrice(0, 1, 50);
			Item.shoot = ModContent.ProjectileType<ElectricSpike>();
			Item.shootSpeed = 32f;
			chargeCost = 4;

			if (ModContent.GetInstance<ServerSideConfig>().areusWeaponsCostMana)
				Item.mana = 10;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 4; // 4 shots
			float rotation = MathHelper.ToRadians(45);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 vel = Vector2.Normalize(player.Center - Main.MouseWorld);
				Vector2 perturbedSpeed = vel.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 8f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			}
				return false;
        }

        public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 10)
				.AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 7)
				.AddTile(ModContent.TileType<AreusForge>())
				.Register();
		}
	}
}