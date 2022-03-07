using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A shard of immense power");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Red;
            Item.width = 36;
            Item.height = 36;
            Item.maxStack = 999;
        }
    }
}