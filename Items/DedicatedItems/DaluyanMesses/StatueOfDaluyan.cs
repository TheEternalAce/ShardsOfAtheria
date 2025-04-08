using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.DaluyanMesses
{
    public class StatueOfDaluyan : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueDungeon;
        }
    }
}
