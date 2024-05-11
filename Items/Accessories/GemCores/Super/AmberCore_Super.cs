using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class AmberCore_Super : ModItem
    {
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
                .AddIngredient(ModContent.ItemType<AmberCore_Greater>())
                .AddIngredient(ItemID.BeetleHusk, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Gem().amberCape = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<AmberCore_Greater>().UpdateAccessory(player, hideVisual);
            player.maxMinions++;
            player.maxTurrets += 3;
            player.Gem().superAmberCore = true;
        }
    }
}