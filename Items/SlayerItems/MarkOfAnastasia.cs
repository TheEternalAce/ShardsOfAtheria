using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class MarkOfAnastasia : SlayerItem
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
            player.statDefense += 30;
            player.GetDamage(DamageClass.Generic) += 0.1f;
            player.statLifeMax2 += 50;
            player.statManaMax2 += 20;
            player.GetModPlayer<SoAPlayer>().markOfAnastasia = true;
        }
    }
}