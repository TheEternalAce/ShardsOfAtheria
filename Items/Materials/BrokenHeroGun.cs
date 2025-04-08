using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class BrokenHeroGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.maxStack = 9999;

            Item.rare = ItemDefaults.RarityPlantera;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
        }
    }
}