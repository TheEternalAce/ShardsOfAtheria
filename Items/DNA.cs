using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class DNA : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DNA");
            Tooltip.SetDefault("'The building blocks of life'\n" +
                "'Short for Deoxyribonucleic Acid'");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.White;
            Item.width = 10;
            Item.height = 18;
            Item.maxStack = 999;
        }
    }
}