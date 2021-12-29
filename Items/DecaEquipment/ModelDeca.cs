using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class ModelDeca : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Model Deca");
            Tooltip.SetDefault("'The new Death has been made, and soon a new dawn shall rise'");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Red;
            Item.width = 54;
            Item.height = 54;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DecaPlayer>().modelDeca = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DecaFragmentA>())
                .AddIngredient(ModContent.ItemType<DecaFragmentB>())
                .AddIngredient(ModContent.ItemType<DecaFragmentC>())
                .AddIngredient(ModContent.ItemType<DecaFragmentD>())
                .AddIngredient(ModContent.ItemType<DecaFragmentE>())
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}