using System.Collections.Generic;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
	public abstract class SpecialItem : ModItem
	{
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(Mod, "SpecialItem", "[c/FF6400:Special Item]"));
		}
	}
}