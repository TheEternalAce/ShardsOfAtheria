using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania;

namespace SagesMania.Items.Accessories
{
	public class ShadowBrand : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Press 'Shadow Cloak' to turn invisible, gaining extra 10% crit chance but losing 10 defense\n" +
				"While not invisible you have a chance to dodge\n" +
				"After dodge you can teleport once");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 22;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
			item.expert = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SMPlayer>().shadowBrand = true;
			if (player.GetModPlayer<SMPlayer>().shadowBrandToggled == 1)
			{
				player.statDefense -= 10;
				player.meleeCrit += 10;
				player.rangedCrit += 10;
				player.magicCrit += 10;
				player.AddBuff(BuffID.Invisibility, 2);
			}
		}
	}
}