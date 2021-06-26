using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories.GemCores
{
	[AutoloadEquip(EquipType.Wings)]
	public class GreaterEmeraldCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Counts as wings\n" +
				"10% increased movement speed\n" +
				"Panic Necklace, Water Walking Boots and Flippers effects\n" +
				"Grants flight and slowfall");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxRunSpeed += .1f;
			player.wingTimeMax = 4 * 60;
			player.panic = true;
			player.waterWalk2 = true;
			player.fireWalk = true;
			player.lavaImmune = true;
			player.lavaTime = 7 * 60;
			player.accFlipper = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<EmeraldCore>());
			recipe.AddIngredient(ItemID.SoulofFlight, 10);
			recipe.AddIngredient(ItemID.WaterWalkingPotion, 15);
			recipe.AddIngredient(ItemID.FlipperPotion, 15);
			recipe.AddIngredient(ItemID.LavaWaders);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 9f;
			acceleration *= 2.5f;
		}
	}
}