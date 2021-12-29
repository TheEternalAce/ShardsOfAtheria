using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
	public class MarkOfAnastasia : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Moderate increase to all stats");
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
			if (player.name == "Sophie" || player.name == "Lilly" || player.name == "Damien"
				|| player.name == "Ariiannah" || player.name == "Arii" || player.name == "Peter"
				|| player.name == "Shane")
			{
				player.statDefense += 30;
				player.GetDamage(DamageClass.Generic) += 0.1f;
				player.moveSpeed += 1;
				player.statLifeMax2 += 50;
				player.statManaMax2 += 20;
			}
			else
			{
				player.statDefense += 15;
				player.GetDamage(DamageClass.Generic) += 0.05f;
				player.moveSpeed += .5f;
				player.statLifeMax2 += 25;
				player.statManaMax2 += 10;
			}
		}
	}
}