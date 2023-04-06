using ShardsOfAtheria.Utilities;
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
            Item.value = 150000;

            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ShardsOfAtheria().rushDrive = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            List<string> phaseKey = SoA.PhaseSwitch.GetAssignedKeys();
            tooltips.Insert(tooltips.GetIndex("Tooltip#"), new TooltipLine(Mod, "Tooltip#", Language.GetTextValue("Mods.ShardsOfAtheria.Common.RushDrive",
                    phaseKey.Count > 0 ? phaseKey[0] : "[Unbounded Hotkey]")));
        }
    }
}