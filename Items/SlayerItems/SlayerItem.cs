using Microsoft.Xna.Framework;
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
			var line = new TooltipLine(Mod, "Slayer Item", "Slayer Item")
			{
				OverrideColor = Color.Red
			};
			tooltips.Add(line);
		}

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			if (Main.hardMode)
			{
				damage += .05f;
			}
			if (NPC.downedQueenSlime)
			{
				damage += .05f;
			}
			if (NPC.downedMechBoss1)
			{
				damage += .1f;
			}
			if (NPC.downedMechBoss2)
			{
				damage += .1f;
			}
			if (NPC.downedMechBoss3)
			{
				damage += .1f;
			}
			if (NPC.downedPlantBoss)
			{
				damage += .15f;
			}
			if (NPC.downedGolemBoss)
			{
				damage += .15f;
			}
			if (NPC.downedFishron)
			{
				damage += .2f;
			}
			if (NPC.downedEmpressOfLight)
			{
				damage += .2f;
			}
			if (NPC.downedAncientCultist)
			{
				damage += .5f;
			}
			if (NPC.downedMoonlord)
			{
				damage += .5f;
			}
		}
    }

	public class SlayerRarity : ModRarity
    {
        public override Color RarityColor => SlayerColor();

		private static Color SlayerColor()
		{
			if (Main.GlobalTimeWrappedHourly % 1f < 0.2f)
				return Color.Red;
			else if (Main.GlobalTimeWrappedHourly % 1f < 0.4f)
				return Color.Lerp(Color.Red, Color.Yellow, (Main.GlobalTimeWrappedHourly % 1f - 0.2f) / 0.2f);
			else if(Main.GlobalTimeWrappedHourly % 1f < 0.6f)
				return Color.Lerp(Color.Yellow, Color.Pink, (Main.GlobalTimeWrappedHourly % 1f - 0.4f) / 0.2f);
			else if(Main.GlobalTimeWrappedHourly % 1f < 0.8f)
				return Color.Lerp(Color.Pink, Color.Yellow, (Main.GlobalTimeWrappedHourly % 1f - 0.6f) / 0.2f);
			else
				return Color.Lerp(Color.Yellow, Color.Red, (Main.GlobalTimeWrappedHourly % 1f - 0.8f) / 0.2f);
		}
    }
}