using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class WeightedDie : ToggleableTool
    {
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            maxMode = 6;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void RightClick(Player player)
        {
            base.RightClick(player);
            var shards = player.Shards();
            shards.weightDie = mode;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var key = this.GetLocalizationKey("Off");
            if (mode > 0)
            {
                tooltips[0].Text += " (" + mode + ")";
            }
            else
            {
                tooltips[0].Text += Language.GetTextValue(key);
            }
        }
    }
}