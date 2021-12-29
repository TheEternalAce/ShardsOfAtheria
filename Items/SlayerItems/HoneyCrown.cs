using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class HoneyCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Calls a honeybee to fight along side you'");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 16;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.Expert;
			Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.AddBuff(ModContent.BuffType<Honeybee>(), 2);
			player.GetModPlayer<SMPlayer>().honeyCrown = true;
		}
    }
}