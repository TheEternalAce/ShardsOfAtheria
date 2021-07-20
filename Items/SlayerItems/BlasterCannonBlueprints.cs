using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.SlayerItems
{
	public class BlasterCannonBlueprints : SlayerItem
	{
		public override void SetDefaults()
		{
			item.width = 50;
			item.height = 22;
			item.value = Item.sellPrice(gold: 7, silver: 50);
			item.rare = ItemRarityID.Expert;
		}
    }
}