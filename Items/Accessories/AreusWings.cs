using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;

namespace SagesMania.Items.Accessories
{
	[AutoloadEquip(EquipType.Wings)]
	public class AreusWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows infinite flight and grants slow fall\n" +
				"Grants immunity to Electrified");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = Item.sellPrice(gold: 15);
			item.rare = ItemRarityID.Cyan;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 1666666666;
			player.buffImmune[BuffID.Electrified] = true;
			player.GetModPlayer<SMPlayer>().areusWings = true;
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

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 20);
			recipe.AddIngredient(ItemID.FragmentVortex, 6);
			recipe.AddTile(ModContent.TileType<AreusForge>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}