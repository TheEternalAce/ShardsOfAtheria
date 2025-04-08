using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.ShardsConditions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class HardlightPrism : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.maxStack = 999;

            Item.rare = ItemDefaults.RarityNova;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void AddRecipes()
        {
            CreateRecipe(2)
                .AddIngredient(ItemID.GoldBar)
                .AddIngredient(ItemID.Feather, 5)
                .AddIngredient<SoulOfDaylight>(5)
                .AddCondition(SoAConditions.DownedNova)
                .AddTile(TileID.SkyMill)
                .Register();
        }
    }
}
