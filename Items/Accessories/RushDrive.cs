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

            Item.rare = ItemRarityID.Blue;
            Item.value = 146500;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ShardsOfAtheria().rushDrive = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int i = 0; i < 5; i++)
            {
                tooltips.Remove(tooltips[tooltips.GetIndex("Tooltip" + i)]);
            }
            List<string> phaseKey = ShardsOfAtheriaMod.PhaseSwitch.GetAssignedKeys();
            string lineText = Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.RushDrive",
                    phaseKey.Count > 0 ? phaseKey[0] : "[Unbounded Hotkey]");
            tooltips.AddTooltip(new TooltipLine(Mod, "Tooltip", lineText));
        }
    }
}
