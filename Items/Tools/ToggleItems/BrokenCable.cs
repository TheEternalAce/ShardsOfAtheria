using System.Collections.Generic;
using ShardsOfAtheria.Common.Items;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class BrokenCable : ToggleableTool
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;

            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = ItemDefaults.ValueDungeon;
            mode = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string mode = "";
            var key = this.GetLocalizationKey(string.Empty);
            if (Active)
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