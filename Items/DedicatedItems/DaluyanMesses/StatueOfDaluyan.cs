using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.DaluyanMesses
{
    public class StatueOfDaluyan : ModItem
    {
        public override void SetStaticDefaults()
        {
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
