using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria.Localization;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class BlasterCanon : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires devastating limes");
		}

		public override void SetDefaults() 
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.width = 40;
			Item.height = 24;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = false;
			Item.crit = 5;
			Item.shoot = ModContent.ProjectileType<NotLime>();
			Item.shootSpeed = 20f;
			Item.rare = ModContent.RarityType<SlayerRarity>();

			Item.mana = 6;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 20)
				.AddIngredient(ItemID.SoulofFright, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}