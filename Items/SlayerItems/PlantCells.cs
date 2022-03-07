using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class PlantCells : SlayerItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Increases minion slots by 4, damage by 10%, defense by 20 and life regen\n" +
				"Further increases while on the surface\n" +
				"'Cells from a failed experiment gone feral'");
        }
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(gold: 7, silver: 50);
			Item.rare = ItemRarityID.Expert;
			Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<SoAPlayer>().plantCells = true;
			player.maxMinions += 4;
			if (player.ZoneOverworldHeight || player.ZoneNormalSpace)
			{
				player.GetDamage(DamageClass.Generic) += .15f;
				player.statDefense += 25;
			}
			else
			{
				player.GetDamage(DamageClass.Generic) += .1f;
				player.statDefense += 20;
			}
		}
    }
}