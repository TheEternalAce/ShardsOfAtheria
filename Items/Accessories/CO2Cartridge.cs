using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Weapons.Ammo;
using System.Collections.Generic;

namespace ShardsOfAtheria.Items.Accessories
{
	public class CO2Cartridge: SpecialItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("CO2 Cartridge");
			Tooltip.SetDefault("Converts BBs into High Velocity Bullets");
		}

		public override void SetDefaults()
		{
			Item.width = 15;
			Item.height = 22;
			Item.value = Item.buyPrice(gold: 5);
			Item.value = Item.sellPrice(silver: 75);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().Co2Cartridge = true;
		}
	}
}