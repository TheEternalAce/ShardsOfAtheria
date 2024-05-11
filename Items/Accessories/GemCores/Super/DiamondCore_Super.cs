using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class DiamondCore_Super : ModItem
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
            Item.defense = 20;

            Item.rare = ItemDefaults.RarityLunaticCultist;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DiamondCore_Greater>())
                .AddIngredient(ItemID.BeetleHusk, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().diamondShield = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<DiamondCore_Greater>().UpdateAccessory(player, hideVisual);
            player.Gem().superDiamondCore = true;
        }
    }
}