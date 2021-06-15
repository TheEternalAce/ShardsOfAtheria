using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SagesMania.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class OrangeMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases ranged damage by 10% and crit chance by 4%\n" +
                "Also works in vanity\n" +
                "Doesn't work properly, it is unobtainable for now");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = ItemRarityID.Orange;
            item.defense = 7;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.1f;
            player.rangedCrit += 4;
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.Pumpkin, 5);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        */
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            base.DrawHair(ref drawHair, ref drawAltHair);
        }
    }
}
