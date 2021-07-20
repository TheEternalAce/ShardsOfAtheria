using Terraria;
using Terraria.ID;

namespace SagesMania.Items.SlayerItems
{
	public class MoonCore : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("50% increased magic damage\n" +
				"Increased maxumim mana by 100\n" +
				"Attacks life steal");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 28;
			item.value = Item.sellPrice(gold: 15);
			item.rare = ItemRarityID.Expert;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magicDamage += .5f;
			player.statManaMax2 += 100;
			player.GetModPlayer<SMPlayer>().moonCore = true;
		}
	}
}