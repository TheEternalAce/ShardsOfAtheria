using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class Virus : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'You don't feel so great...'");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.White;
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UnanalyzedMicrobe>())
                .AddTile(TileID.AlchemyTable)
                .Register();
        }
    }
}