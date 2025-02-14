using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class ZChargedModule : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 28;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Purple;
            Item.value = 100000;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<HardlightPrism>(10)
                .AddIngredient<BionicBarItem>(10)
                .AddIngredient<AreusShard>(5)
                .AddIngredient(ItemID.LunarBar)
                //.AddIngredient<DryskalScale>()
                //.AddIngredient<StaticPlating>()
                .Register();
        }
    }
}