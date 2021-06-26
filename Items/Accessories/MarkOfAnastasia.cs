using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	public class MarkOfAnastasia : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Moderate increase to all stats");
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
			if (player.name == "Sophie" || player.name == "Lilly" || player.name == "Damien"
				|| player.name == "Ariiannah" || player.name == "Arii" || player.name == "Peter"
				|| player.name == "Shane")
			{
				player.statDefense += 30;
				player.allDamage += 0.1f;
				player.maxRunSpeed += 20;
				player.statLifeMax2 += 50;
				player.statManaMax2 += 20;
			}
			else
			{
				player.statDefense += 15;
				player.allDamage += 0.05f;
				player.maxRunSpeed += 10;
				player.statLifeMax2 += 25;
				player.statManaMax2 += 10;
			}
		}
	}
}