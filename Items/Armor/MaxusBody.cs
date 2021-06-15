using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SagesMania.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MaxusBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("5% increased damage\n" +
                "+5% critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 30;
            item.rare = ItemRarityID.Red;
            item.defense = 48;
        }
        public override void UpdateEquip(Player player)
        {
            player.allDamage += 0.05f;
            player.magicCrit += 5;
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<WaterSnakeTrap>());
            recipe.AddIngredient(ModContent.ItemType<LightSnakeTrap>());
            recipe.AddIngredient(ModContent.ItemType<FireSpider>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        */

        public override bool DrawBody()
        {
            return false;
        }
    }
}
