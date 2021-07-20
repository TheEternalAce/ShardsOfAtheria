using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;
using System.Collections.Generic;

namespace SagesMania.Items.Accessories
{
	public class ValkyrieCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Attacks shock enemies briefly");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 1);
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<SMPlayer>().valkyrieCrown = true;
        }
    }
}