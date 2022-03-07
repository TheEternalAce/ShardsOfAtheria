using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
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
			Item.width = 22;
			Item.height = 28;
			Item.value = Item.sellPrice(gold: 15);
			Item.rare = ItemRarityID.Expert;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Magic) += .5f;
			player.statManaMax2 += 100;
			player.GetModPlayer<SoAPlayer>().moonCore = true;
		}
	}
}