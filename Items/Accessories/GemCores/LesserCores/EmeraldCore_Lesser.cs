using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.LesserCores
{
    public class EmeraldCore_Lesser : ModItem
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

            Item.value = Item.sellPrice(0, 0, 15);
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(ShardsRecipes.Gold, 10)
                .AddIngredient(ItemID.StoneBlock, 10)
                .AddIngredient(ItemID.Emerald, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpBoost = true;
            player.ShardsOfAtheria().lesserEmeraldCore = true;
        }
    }
}