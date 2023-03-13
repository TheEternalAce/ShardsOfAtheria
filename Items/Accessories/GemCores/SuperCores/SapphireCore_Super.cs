using ShardsOfAtheria.Items.Accessories.GemCores.GreaterCores;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.SuperCores
{
    public class SapphireCore_Super : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;

            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 3);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SapphireCore_Greater>())
                .AddIngredient(ItemID.FragmentStardust, 5)
                .AddIngredient(ItemID.FragmentVortex, 5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ShardsOfAtheria().sapphireSpirit = !hideVisual;
            player.ShardsOfAtheria().superSapphireCore = true;
            player.maxMinions += 5;
            player.AddBuff(BuffID.Thorns, 0);
        }
    }
}