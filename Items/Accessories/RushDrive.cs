using ShardsOfAtheria.Utilities;
using ShardsOfAtheria.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class RushDrive : ModItem
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

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ShardsOfAtheria().rushDrive = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(ShardsRecipes.EvilBar, 15)
                .AddRecipeGroup(ShardsRecipes.EvilMaterial, 5)
                .AddIngredient(ItemID.Bone, 5)
                .AddTile(TileID.Hellforge)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "tip", string.Format(Language.GetTextValue("Mods.ShardsOfAtheria.Common.RushDrive"),
                    ShardsOfAtheria.PhaseSwitch.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.PhaseSwitch.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));
        }
    }
}