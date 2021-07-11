using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Weapons.Ammo;
using System.Collections.Generic;

namespace SagesMania.Items.Accessories
{
	public class CO2Cartridge: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("CO2 Cartridge");
			Tooltip.SetDefault("Converts BBs into High Velocity Bullets");
		}

		public override void SetDefaults()
		{
			item.width = 15;
			item.height = 22;
			item.value = Item.buyPrice(gold: 5);
			item.value = Item.sellPrice(silver: 75);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().Co2Cartridge = true;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "Special Item", "[c/FF6400:Special Item]"));
		}
	}
}