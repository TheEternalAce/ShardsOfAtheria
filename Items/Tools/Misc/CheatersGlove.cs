using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public class CheatersGlove : ModItem
    {
        public int cheatSide = 6;

        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.rare = ItemDefaults.RarityAreus;
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
            shards.cheatGlove = cheatSide;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var key = this.GetLocalizationKey(string.Empty) + "Off";
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