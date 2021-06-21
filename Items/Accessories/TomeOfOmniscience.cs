using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	public class TomeOfOmniscience: ModItem
	{
        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Press 'Cycle Knowledge Base' to cycle between 3 Knowledge Bases:\n" +
				"Combat Conservation and Exploration");
		}

        public override void SetDefaults()
		{
			item.width = 15;
			item.height = 22;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().omnicientTome = true;
		}
	}
}