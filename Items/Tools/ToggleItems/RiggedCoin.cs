using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class RiggedCoin : ToggleableTool
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            maxMode = 2;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateInventory(Player player)
        {
            var shards = player.Shards();
            shards.riggedCoin = mode;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string suffix = "";
            var key = this.GetLocalizationKey(string.Empty);
            if (mode == 0)
            {
                suffix = Language.GetTextValue(key + "Off");
            }
            if (mode == 1)
            {
                suffix = Language.GetTextValue(key + "Tails");
            }
            if (mode == 2)
            {
                suffix = Language.GetTextValue(key + "Heads");
            }
            tooltips[0].Text += suffix;
        }
    }
}