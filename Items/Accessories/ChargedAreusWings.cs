using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Accessories
{
	[AutoloadEquip(EquipType.Wings)]
	public class ChargedAreusWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows infinite flight and grants slow fall\n" +
				"Grants immunity to Electrified\n" +
				"20% increased areus damage and all attacks inflict electrified\n" +
				"Increases Areus Charge by 50 and Areus Charge regenerates");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = Item.sellPrice(gold: 15);
			Item.rare = ItemRarityID.Cyan;
			Item.accessory = true;
		}
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 2000000000;
			player.buffImmune[BuffID.Electrified] = true;
			player.GetModPlayer<SMPlayer>().areusBatteryElectrify = true;
			player.GetModPlayer<SMPlayer>().areusWings = true;
			player.GetModPlayer<SMPlayer>().naturalAreusRegen = true;
			player.GetModPlayer<SMPlayer>().areusResourceMax2 += 100;
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
			speed = 18f;
			acceleration *= 5f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusWings>())
				.AddIngredient(ModContent.ItemType<AreusBattery>())
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 10)
				.AddTile(ModContent.TileType<AreusForge>())
				.Register();
		}
	}
}