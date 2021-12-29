using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
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
			Item.width = 52;
			Item.height = 32;
			Item.value = Item.sellPrice(gold: 15);
			Item.accessory = true;
			Item.rare = ItemRarityID.Expert;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (Main.dayTime)
            {
				player.statDefense /= 2;
                player.GetDamage(DamageClass.Generic) += .5f;
            }
            else player.GetDamage(DamageClass.Generic) += .25f;
			player.statManaMax2 += 40;
		}
	}
}