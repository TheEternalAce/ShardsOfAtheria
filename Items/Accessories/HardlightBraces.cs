using MMZeroElements;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
	public class HardlightBraces : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
			WeaponElements.Electric.Add(Type);
			WeaponElements.Metal.Add(Type);
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.height = 22;
			Item.accessory = true;

			Item.damage = 26;
			Item.knockBack = 4;
			Item.crit = 2;

			Item.shoot = ModContent.ProjectileType<HardlightBlade>();
			Item.shootSpeed = 16;

			Item.rare = ItemRarityID.Green;
			Item.value = Item.buyPrice(0, 10);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.ShardsOfAtheria().hardlightBraces = true;
			player.statDefense += 8;
			player.wingTimeMax += 10;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ChargedFeather>(), 15)
				.AddRecipeGroup(ShardsRecipes.Gold, 6)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}