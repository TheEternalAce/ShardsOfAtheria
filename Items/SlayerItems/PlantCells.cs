using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.SlayerItems
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
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 7, silver: 50);
			item.rare = ItemRarityID.Expert;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<SMPlayer>().plantCells = true;
			player.maxMinions += 4;
			if (player.ZoneOverworldHeight)
			{
				player.allDamage += .15f;
				player.statDefense += 25;
			}
			else
			{
				player.allDamage += .1f;
				player.statDefense += 20;
			}
		}
    }
}