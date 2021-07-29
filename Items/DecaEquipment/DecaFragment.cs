using SagesMania.Buffs;
using SagesMania.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DecaEquipment
{
    public abstract class DecaFragment : ModItem
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Dega Fragment", "[c/FF4100:Deca Fragment]"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BionicBarItem>());
            recipe.AddIngredient(ModContent.ItemType<DeathEssence>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}