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
                "7 defense");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = ItemRarityID.Orange;
            item.vanity = true;
        }

        public override void UpdateVanity(Player player, EquipType type)
        {
            player.GetModPlayer<SMPlayer>().OrangeMask = true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 2);
            recipe.AddIngredient(ItemID.Pumpkin, 5);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }
    }
}
