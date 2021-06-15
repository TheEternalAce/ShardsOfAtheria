using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items
{
	public class BrokenHeroGun : ModItem
	{
		public override void SetStaticDefaults() 
		{
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 38;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 99;
		}
    }
}