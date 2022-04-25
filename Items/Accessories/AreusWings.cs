using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using Terraria.DataStructures;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Accessories
{
	[AutoloadEquip(EquipType.Wings)]
	public class AreusWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows infinite flight and grants slow fall\n" +
				"Grants immunity to Electrified");

			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(180, 9f, 2.5f);
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = Item.sellPrice(0,  15);
			Item.rare = ItemRarityID.Cyan;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[BuffID.Electrified] = true;
			player.GetModPlayer<SoAPlayer>().areusWings = true;
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
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 20)
				.AddIngredient(ItemID.FragmentVortex, 6)
				.AddTile(ModContent.TileType<AreusForge>())
				.Register();
		}
	}
}