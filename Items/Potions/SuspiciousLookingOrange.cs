using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Potions
{
    public class SuspiciousLookingOrange : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Sus orange");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.consumable = true;
            item.useTurn = true;
            item.buffType = BuffID.Ironskin;
            item.buffTime = (4 * 60) * 60;
        }

        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.Regeneration, (4 * 60) * 60);
            player.AddBuff(BuffID.Swiftness, (4 * 60) * 60);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Submission Item", "Submitted by [c/9000FF:Torra th]"));
        }
    }
}