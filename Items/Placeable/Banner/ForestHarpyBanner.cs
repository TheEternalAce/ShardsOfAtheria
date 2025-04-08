using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Tiles.Banner;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable.Banner
{
    public class ForestHarpyBanner : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<MonsterBanners>(), MonsterBanners.ForestHarpyBanner);

            Item.rare = ItemDefaults.RarityBanner;
            Item.value = ItemDefaults.ValueBuffPotion;
        }
    }
}