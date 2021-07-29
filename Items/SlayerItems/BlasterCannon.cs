using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using SagesMania.Projectiles;
using Microsoft.Xna.Framework;
using SagesMania.Tiles;

namespace SagesMania.Items.SlayerItems
{
	public class BlasterCannon : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires devastating limes");
		}

		public override void SetDefaults() 
		{
			item.damage = 100;
			item.magic = true;
			item.noMelee = true;
			item.width = 40;
			item.height = 24;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item11;
			item.autoReuse = false;
			item.crit = 20;
			item.value = Item.sellPrice(gold: 25);
			item.shoot = ModContent.ProjectileType<NotLime>();
			item.shootSpeed = 20f;

			item.mana = 6;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BlasterCannonBlueprints>());
			recipe.AddIngredient(ItemID.HallowedBar, 20);
			recipe.AddIngredient(ItemID.SoulofFright, 10);
			recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}