using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Potions
{
    public class SuspiciousLookingOrange : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Sus orange");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = true;
            Item.buffType = BuffID.Ironskin;
            Item.buffTime = (4 * 60) * 60;
        }

        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.Regeneration, (4 * 60) * 60);
            player.AddBuff(BuffID.Swiftness, (4 * 60) * 60);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Submission Item", "Submitted by [c/9000FF:Torra th]"));
        }
    }
}