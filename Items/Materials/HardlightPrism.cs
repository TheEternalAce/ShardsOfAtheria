using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class HardlightPrism : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.maxStack = 999;

            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 1, 50);
        }
    }
}
