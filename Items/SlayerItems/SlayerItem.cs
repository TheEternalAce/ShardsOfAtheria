using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.SlayerItems
{
	public abstract class SlayerItem : ModItem
	{
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (item.damage > 0)
				tooltips.Add(new TooltipLine(mod, "Damage", "Damage scales with progression"));
			tooltips.Add(new TooltipLine(mod, "Slayer Item", "[c/FF0000:Slayer Item]"));
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.hardMode)
			{
				add += .1f;
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				add += .15f;
			}
			if (NPC.downedPlantBoss)
			{
				add += .2f;
			}
			if (NPC.downedGolemBoss)
			{
				add += .2f;
			}
			if (NPC.downedAncientCultist)
			{
				add += .5f;
			}
			if (NPC.downedMoonlord)
			{
				add += 1f;
			}
		}
	}
}