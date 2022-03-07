using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class Bacteria : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'It can either make you really sick, or help with bodily functions'");
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