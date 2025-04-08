using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Tiles.Banner;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable.Banner
{
    public class HallowedHarpyBanner : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<MonsterBanners>(), MonsterBanners.HallowedHarpyBanner);

            Item.rare = ItemDefaults.RarityBanner;
            Item.value = ItemDefaults.ValueBuffPotion;
        }
    }
}