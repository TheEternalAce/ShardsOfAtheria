using ShardsOfAtheria.Items.Accessories.GemCores.Cores;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.GreaterCores
{
    public class SapphireCore_Greater : ModItem
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

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SapphireCore>())
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ShardsOfAtheria().sapphireSpirit = !hideVisual;
            player.ShardsOfAtheria().sapphireCore = true;

            player.AddBuff(BuffID.Thorns, 2);
        }
    }
}