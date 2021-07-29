using SagesMania.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DecaEquipment
{
    public abstract class DecaEquipment : ModItem
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Deca Gear", "[c/FF4100:Deca Equipment]"));
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<DecaPlayer>().fullDeca;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BionicBarItem>(), 20);
            recipe.AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 10);
            recipe.AddIngredient(ItemID.SoulofFlight, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofSight, 10);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DeathEssence>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}