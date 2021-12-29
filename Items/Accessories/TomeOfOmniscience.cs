using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
	public class TomeOfOmniscience: ModItem
	{
        public override void SetStaticDefaults()
		{
		}

        public override void SetDefaults()
		{
			Item.width = 15;
			Item.height = 22;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
			Item.expert = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().omnicientTome = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			var list = ShardsOfAtheria.TomeKey.GetAssignedKeys();
			string keyname = "Not bound";

			if (list.Count > 0)
			{
				keyname = list[0];
			}

			tooltips.Add(new TooltipLine(Mod, "Damage", $"Press '[i:{keyname}]' to cycle between 3 Knowledge Bases:\n" +
				"Combat Conservation and Exploration"));
		}
	}
}