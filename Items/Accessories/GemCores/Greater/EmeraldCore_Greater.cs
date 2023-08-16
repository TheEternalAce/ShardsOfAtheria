using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Greater
{
    public class EmeraldCore_Greater : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmeraldCore>())
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.Flipper)
                .AddIngredient(ItemID.PanicNecklace)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Shards().emeraldBoots = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Frostspark Boots
            player.accRunSpeed = 6.75f;
            player.rocketBoots = player.vanityRocketBoots = 3;
            player.iceSkate = true;

            // Misc
            player.accFlipper = true;
            player.jumpBoost = true;
            player.wingTimeMax += 20;
            player.Shards().emeraldBoots = !hideVisual;
        }
    }
}