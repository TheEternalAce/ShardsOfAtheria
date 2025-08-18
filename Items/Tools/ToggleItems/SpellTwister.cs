using ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor;
using ShardsOfAtheria.Common.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public class SpellTwister : ToggleableTool
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 48;

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

        public override void UpdateInventory(Player player)
        {
            if (Active)
            {
                player.buffImmune[ModContent.BuffType<ShadeState>()] = true;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string suffix = "";
            var key = this.GetLocalizationKey(string.Empty);
            if (Active)
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