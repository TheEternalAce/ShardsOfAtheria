using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class BrokenHeroGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The broken gun of a long forgotten hero'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 4, 50);
        }
    }
}