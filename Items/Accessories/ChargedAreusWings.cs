using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using SagesMania.Items.AreusDamageClass;

namespace SagesMania.Items.Accessories
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
			item.width = 22;
			item.height = 20;
			item.value = Item.sellPrice(gold: 15);
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 2000000000;
			player.buffImmune[BuffID.Electrified] = true;
			player.GetModPlayer<SMPlayer>().areusBatteryElectrify = true;
			player.GetModPlayer<SMPlayer>().areusWings = true;
			player.GetModPlayer<AreusDamagePlayer>().naturalAreusRegen = true;
			player.GetModPlayer<AreusDamagePlayer>().areusResourceMax2 += 100;
			player.GetModPlayer<AreusDamagePlayer>().areusDamageMult += .1f;
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
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusWings>());
			recipe.AddIngredient(ModContent.ItemType<AreusBattery>());
			recipe.AddIngredient(ModContent.ItemType<AreusOreItem>(), 10);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}