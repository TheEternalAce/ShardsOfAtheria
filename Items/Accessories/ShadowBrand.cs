using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria;
using System.Collections.Generic;

namespace ShardsOfAtheria.Items.Accessories
{
	public class ShadowBrand : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var list = ShardsOfAtheria.ShadowCloak.GetAssignedKeys();
			string keyname = "Not bound";

			if (list.Count > 0)
			{
				keyname = list[0];
			}

			tooltips.Add(new TooltipLine(Mod, "Tip", $"Press '[i:{keyname}]' to turn invisible, gaining extra 10% crit chance but losing 10 defense\n" +
				"While not invisible you have a chance to dodge\n" +
				"After dodge you can teleport once"));
		}

        public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 22;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
			Item.expert = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SoAPlayer>().shadowBrand = true;
			if (player.GetModPlayer<SoAPlayer>().shadowBrandToggled)
			{
				player.statDefense -= 10;
				player.GetCritChance(DamageClass.Generic) += 10;
				player.AddBuff(BuffID.Invisibility, 2);
			}
		}
	}
}