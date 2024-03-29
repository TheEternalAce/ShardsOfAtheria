using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class WeightedDie : ModItem
    {
        public int cheatSide = 6;

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
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
            cheatSide--;
            if (cheatSide < 0)
            {
                cheatSide = 6;
            }
            var shards = player.Shards();
            shards.weightDie = cheatSide;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var key = this.GetLocalizationKey("Off");
            if (cheatSide > 0)
            {
                tooltips[0].Text += " (" + (cheatSide) + ")";
            }
            else
            {
                tooltips[0].Text += Language.GetTextValue(key);
            }
        }
    }
}