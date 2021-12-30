using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	[AutoloadEquip(EquipType.Wings)]
	public class EmeraldCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Counts as wings\n" +
				"10% increased movement speed\n" +
				"Increased Jump height\n" +
				"Panic Neclace effect\n" +
				"Grants flight and slowfall");

			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(30, .5f, .5f);
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<LesserEmeraldCore>())
				.AddIngredient(ItemID.Cloud, 20)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.moveSpeed += .1f;
			player.panic = true;
			player.jumpBoost = true;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		    {
			ascentWhenFalling = .85f;
			ascentWhenRising = .5f;
			maxCanAscendMultiplier = 3f;
			maxAscentMultiplier = 1f;
			constantAscend = 0.135f;
		}
	}
}