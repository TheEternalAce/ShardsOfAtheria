using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;

namespace SagesMania.Items.SlayerItems
{
	public class HoneyCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Calls a honeybee to fight along side you'");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 16;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.Expert;
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.AddBuff(ModContent.BuffType<Honeybee>(), 2);
			player.GetModPlayer<SMPlayer>().honeyCrown = true;
		}
    }
}