using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DevItems.DaluyanMesses
{
    public class StatueOfDaluyan : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Great for impersonating devs!'\n" +
                "'bro legit i have the perfect dev item'\n" +
                "'That's Fool's Gold! That ain't worth nothing!'");

            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.rare = ItemRarityID.Cyan;
        }
    }
}
