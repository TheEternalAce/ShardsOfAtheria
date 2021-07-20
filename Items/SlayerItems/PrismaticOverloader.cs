using Terraria;
using Terraria.ID;

namespace SagesMania.Items.SlayerItems
{
	public class PrismaticOverloader : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("25% increased damage\n" +
				"Damage is doubled during the daytime at the cost of 50% reduced defense\n" +
				"Increased maximum mana by 40");
		}

		public override void SetDefaults()
		{
			item.width = 52;
			item.height = 32;
			item.value = Item.sellPrice(gold: 15);
			item.accessory = true;
			item.rare = ItemRarityID.Expert;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (Main.dayTime)
            {
				player.statDefense /= 2;
                player.allDamage += .5f;
            }
            else player.allDamage += .25f;
			player.statManaMax2 += 40;
		}
	}
}