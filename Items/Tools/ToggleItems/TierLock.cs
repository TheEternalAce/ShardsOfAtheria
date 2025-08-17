using ShardsOfAtheria.Common.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class TierLock : ToggleableTool
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void RightClick(Player player)
        {
            maxMode = 1;
            if (NPC.downedBoss3) maxMode++;
            if (Main.hardMode) maxMode++;
            if (NPC.downedGolemBoss) maxMode++;
            if (NPC.downedMoonlord) maxMode++;
            base.RightClick(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var key = this.GetLocalizationKey("Off");
            if (mode > 0)
            {
                tooltips[0].Text += " (Level: " + mode + ", ";
                if (mode == 1) tooltips[0].Text += "Early";
                if (mode == 2) tooltips[0].Text += "Skeletron";
                if (mode == 3) tooltips[0].Text += "Hardmode";
                if (mode == 4) tooltips[0].Text += "Golem";
                if (mode == 5) tooltips[0].Text += "Endgame";
                tooltips[0].Text += ")";
            }
            else tooltips[0].Text += Language.GetTextValue(key);
        }
    }
}