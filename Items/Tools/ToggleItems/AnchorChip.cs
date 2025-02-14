using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class AnchorChip : ToggleableTool
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = ItemDefaults.ValueDungeon;
            mode = 1;
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string suffix = "";
            var key = this.GetLocalizationKey(string.Empty);
            if (mode > 0)
            {
                suffix = Language.GetOrRegister(key + "On").ToString();
            }
            else
            {
                suffix = Language.GetOrRegister(key + "Off").ToString();
            }
            tooltips[0].Text += suffix;
        }
    }
}