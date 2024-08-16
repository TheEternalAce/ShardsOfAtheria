using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class JumperCables : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 22;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityPlantera;
            Item.value = 156000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().jumperCable = true;
        }
    }
}