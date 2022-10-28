using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class LunaticSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Summons a magic circle behind you that fires ice fragments at your cursor\n" +
                "Gives a chance to dodge attacks\n" +
                "Every dodge increases ice fragments fired by 1, up to 5 total";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SoulTeleport", string.Format("Press {0} to teleport", ShardsOfAtheria.SoulTeleport.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.SoulTeleport.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));

            base.ModifyTooltips(tooltips);
        }
    }
}
