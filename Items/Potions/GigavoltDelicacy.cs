using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Potions
{
	public class GigavoltDelicacy : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 30;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 50;
			Item.maxStack = 9999;

			Item.DefaultToPotion(BuffID.WellFed2, 8.Minutes());
			SoAGlobalItem.Potions.Remove(Type);

			Item.value = 15000;
			Item.rare = ItemRarityID.Cyan;
		}
	}
}
