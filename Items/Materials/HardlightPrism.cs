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

            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 1, 50);
        }

        public override void AddRecipes()
        {
            CreateRecipe(2)
                .AddIngredient(ItemID.GoldBar)
                .AddIngredient<SoulOfDaylight>(15)
                .AddCondition(SoAConditions.DownedNova)
                .AddTile(TileID.SkyMill)
                .Register();
        }
    }
}
