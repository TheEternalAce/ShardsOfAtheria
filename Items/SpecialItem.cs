using System.Collections.Generic;
using Terraria.ModLoader;

namespace SagesMania.Items
{
	public abstract class SpecialItem : ModItem
	{
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "SpecialItem", "[c/FF6400:Special Item]"));
		}
	}
}