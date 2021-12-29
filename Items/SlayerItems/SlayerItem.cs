using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public abstract class SlayerItem : ModItem
	{
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (Item.damage > 0)
				tooltips.Add(new TooltipLine(Mod, "Damage", "Damage scales with progression"));
			tooltips.Add(new TooltipLine(Mod, "Slayer Item", "[c/FF0000:Slayer Item]"));
		}

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
		{
			if (Main.hardMode)
			{
				damage += .1f;
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				damage += .15f;
			}
			if (NPC.downedPlantBoss)
			{
				damage += .2f;
			}
			if (NPC.downedGolemBoss)
			{
				damage += .2f;
			}
			if (NPC.downedAncientCultist)
			{
				damage += .5f;
			}
			if (NPC.downedMoonlord)
			{
				damage += 1f;
			}
		}
    }
}