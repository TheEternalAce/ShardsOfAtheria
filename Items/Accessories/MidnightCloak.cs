using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class MidnightCloak : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 22;
            Item.accessory = true;
            Item.expert = true;

            Item.rare = ItemRarityID.Red;
            Item.value = 321000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().deathCloak = true;
        }
    }
}