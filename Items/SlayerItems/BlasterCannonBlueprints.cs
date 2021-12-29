using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class BlasterCannonBlueprints : SlayerItem
	{
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 22;
			Item.value = Item.sellPrice(gold: 7, silver: 50);
			Item.rare = ItemRarityID.Expert;
		}
    }
}