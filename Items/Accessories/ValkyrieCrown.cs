using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using System.Collections.Generic;

namespace ShardsOfAtheria.Items.Accessories
{
	public class ValkyrieCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Attacks shock enemies briefly");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(0,  1);
			Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<SoAPlayer>().valkyrieCrown = true;
        }
    }
}