using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace ShardsOfAtheria.Items.DevItems.DaluyanMesses
{
    public class StatueOfDaluyan : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Great for impersonating devs!'\n" +
                "'bro legit i have the perfect dev item'\n" +
                "'That's Fool's Gold! That ain't worth nothing!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.rare = ItemRarityID.Cyan;
        }
    }
}
