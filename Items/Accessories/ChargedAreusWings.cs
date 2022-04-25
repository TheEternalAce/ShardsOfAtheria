using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Terraria.DataStructures;

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
				"Areus Charge regenerates");

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
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[BuffID.Electrified] = true;
			player.GetModPlayer<SoAPlayer>().areusBatteryElectrify = true;
			player.GetModPlayer<SoAPlayer>().areusWings = true;
			player.GetModPlayer<SoAPlayer>().naturalAreusRegen = true;
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
	}
}