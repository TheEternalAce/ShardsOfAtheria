using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class AnchorChip : ModItem
    {
        public bool active = true;

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void RightClick(Player player)
        {
            active = !active;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string mode = "";
            var key = this.GetLocalizationKey(string.Empty);
            if (active)
            {
                mode = Language.GetOrRegister(key + "On").ToString();
            }
            else
            {
                mode = Language.GetOrRegister(key + "Off").ToString();
            }
            tooltips[0].Text += mode;
        }
    }
}