using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Tiles.Banner;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Placeable.Banner
{
    public class DesertHarpyBanner : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<MonsterBanners>(), MonsterBanners.DesertHarpyBanner);

            Item.rare = ItemDefaults.RarityBanner;
            Item.value = ItemDefaults.ValueBuffPotion;
        }
    }
}