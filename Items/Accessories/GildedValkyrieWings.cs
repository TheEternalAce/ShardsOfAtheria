using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	[AutoloadEquip(EquipType.Wings)]
	public class GildedValkyrieWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'The gold plating on the wings weighs you down'");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Expert;
			item.accessory = true;
			item.expert = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 30;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		    {
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1.5f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 5f;
			acceleration *= 1.5f;
		}
	}
}