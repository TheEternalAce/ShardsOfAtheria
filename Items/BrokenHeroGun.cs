using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items
{
	public class BrokenHeroGun : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = Item.sellPrice(gold: 7, silver: 50);
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 99;
		}
    }
}