using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	[AutoloadEquip(EquipType.Wings)]
	public class EmeraldCore_Greater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greater Emerald Core");
			Tooltip.SetDefault("Counts as wings\n" +
				"10% increased movement speed\n" +
				"Increased jump height" +
				"Panic Necklace, Lava Waders and Flippers effects\n" +
				"Grants flight and slowfall");

			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(120, 9f, 2.5f);
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.jumpBoost = true;
			player.panic = true;
			player.accFlipper = true;
			player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 420;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<EmeraldCore>())
				.AddIngredient(ItemID.HallowedBar, 5)
				.AddIngredient(ItemID.Flipper)
				.AddIngredient(ItemID.LavaWaders)
				.AddIngredient(ItemID.PanicNecklace)
				.AddTile(TileID.MythrilAnvil)
				.Register();
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