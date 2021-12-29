using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ShardsOfAtheria.Projectiles;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class BlasterCannon : SlayerItem
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
			Item.rare = ItemRarityID.Expert;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = false;
			Item.crit = 20;
			Item.value = Item.sellPrice(gold: 25);
			Item.shoot = ModContent.ProjectileType<NotLime>();
			Item.shootSpeed = 20f;

			Item.mana = 6;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BlasterCannonBlueprints>())
				.AddIngredient(ItemID.HallowedBar, 20)
				.AddIngredient(ItemID.SoulofFright, 10)
				.AddTile(ModContent.TileType<CobaltWorkbench>())
				.Register();
		}
	}
}