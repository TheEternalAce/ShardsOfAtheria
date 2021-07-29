using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items
{
    public class DeathEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The essence of Death");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Red;
            item.width = 32;
            item.height = 32;
            item.maxStack = 999;
        }
    }
}