using SagesMania.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
    public class RushDrive : SpecialItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Gives the user a ''phase 2'' when below 50% life");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SMPlayer>().rushDrive = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<PhaseBarItem>(), 10);
            recipe.AddRecipeGroup("SM:EvilBars", 5);
            recipe.AddIngredient(ItemID.TissueSample, 5);
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var list = SagesMania.PhaseSwitch.GetAssignedKeys();
            string keyname = "Not bound";

            if (list.Count > 0)
            {
                keyname = list[0];
            }

            tooltips.Add(new TooltipLine(mod, "tip", $"Press '[i:{keyname}]' to chose between two phase types:\n" +
                "Offensive: Sacrifice half of total defense for doubled damage and 20% increased crit chance\n" +
                "Defensive: Sacrifice half of total damage for doubled defense and 20% reduced damage\n" +
                "Always get 20% increased movement speed"));
        }
    }
}