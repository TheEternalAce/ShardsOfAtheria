using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public abstract class DecaFragment : ModItem
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "DecaFragment2", "Unite with the other fragments to unlock their power"));
            tooltips.Add(new TooltipLine(Mod, "DecaFragment1", "[c/FF4100:Deca Fragment]"));
        }
    }
}