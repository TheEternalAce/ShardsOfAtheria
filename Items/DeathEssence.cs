using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class DeathEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The essence of Death");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Red;
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
        }
    }
}