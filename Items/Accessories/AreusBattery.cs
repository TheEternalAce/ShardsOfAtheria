using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Accessories
{
	public class AreusBattery: ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("10% increased areus damage and all attacks inflict Electrified\n" +
				"Grants immunity to Electrified\n" +
				"Areus Charge regenerates");
		}

		public override void SetDefaults()
		{
			Item.width = 15;
			Item.height = 20;
			Item.value = Item.sellPrice(gold: 15);
			Item.rare = ItemRarityID.Cyan;
			Item.accessory = true;
		}
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[BuffID.Electrified] = true;
			player.GetModPlayer<SoAPlayer>().areusBatteryElectrify = true;
			player.GetModPlayer<SoAPlayer>().naturalAreusRegen = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 5)
				.AddIngredient(ItemID.ChlorophyteBar, 10)
				.AddIngredient(ItemID.SoulofMight, 10)
				.AddIngredient(ItemID.MechanicalBatteryPiece)
				.AddTile(ModContent.TileType<AreusForge>())
				.Register();
		}
	}
}