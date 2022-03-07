using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class CancerCell : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The disease, our own bodies'");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.White;
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UnanalyzedMicrobe>())
                .AddTile(TileID.AlchemyTable)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
}