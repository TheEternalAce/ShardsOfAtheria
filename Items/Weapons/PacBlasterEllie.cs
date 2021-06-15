using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class PacBlasterEllie : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("A certain Root Beer addict's friend");
		}

		public override void SetDefaults() 
		{
			item.damage = 126;
			item.magic = true;
			item.noMelee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/BlasterShoot");
			item.autoReuse = true;
			item.crit = 6;
			item.shoot = mod.ProjectileType("FirePacBlasterShot");
			item.shootSpeed = 20;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 15);
			recipe.AddIngredient(ItemID.Ichor, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PacBlasterCharles"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			target.AddBuff(BuffID.Frostburn, 600);
		}

		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.Bullet) // or ProjectileID.WoodenArrowFriendly
			{
				type = ProjectileID.BulletHighVelocity; // or ProjectileID.FireArrow;
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}*/
	}
}