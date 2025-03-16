using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class EmeraldCore_Super : ModItem
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

            Item.rare = ItemDefaults.RarityLunaticCultist;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmeraldCore_Greater>())
                .AddIngredient(ItemID.BeetleHusk, 10)
                .AddIngredient(ItemID.SoulofFlight, 20)
                .AddIngredient(ItemID.LargeEmerald)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().emeraldBoots = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<EmeraldCore_Greater>().UpdateAccessory(player, hideVisual);
            player.moveSpeed += 0.05f;
            player.wingTimeMax += 15;
            player.Gem().superEmeraldCore = true;
            player.Gem().superGemCore = true;
        }
    }
}