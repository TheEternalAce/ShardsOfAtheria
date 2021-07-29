using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DecaEquipment
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
            item.rare = ItemRarityID.Red;
            item.width = 54;
            item.height = 54;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<DecaPlayer>().fullDeca = true;
            player.GetModPlayer<DecaPlayer>().decaFragmentA = true;
            player.GetModPlayer<DecaPlayer>().decaFragmentB = true;
            player.GetModPlayer<DecaPlayer>().decaFragmentC = true;
            player.GetModPlayer<DecaPlayer>().decaFragmentD = true;
            player.GetModPlayer<DecaPlayer>().decaFragmentE = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<DecaFragmentA>());
            recipe.AddIngredient(ModContent.ItemType<DecaFragmentB>());
            recipe.AddIngredient(ModContent.ItemType<DecaFragmentC>());
            recipe.AddIngredient(ModContent.ItemType<DecaFragmentD>());
            recipe.AddIngredient(ModContent.ItemType<DecaFragmentE>());
            recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}