using ShardsOfAtheria.Tiles.Furniture.Trophies.Master;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable.Furniture.Trophies.Master
{
    public class DeathRelic : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            // The place style (here by default 0) is important if you decide to have more than one relic share the same tile type (more on that in the tiles' code)
            Item.DefaultToPlaceableTile(ModContent.TileType<DeathRelicTile>(), 0);

            Item.width = 30;
            Item.height = 40;
            Item.maxStack = 99;
            Item.master = true;

            Item.rare = ItemRarityID.Master;
            Item.value = ItemDefaults.ValueRelicTrophy;
        }
    }
}