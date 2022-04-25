using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public abstract class SlayerItem : ModItem
	{
        public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			Item.rare = ItemRarityID.Yellow;
		}

        public override void UpdateInventory(Player player)
		{
			Item.rare = ItemRarityID.Yellow;
		}

		public override void HoldItem(Player player)
		{
			Item.rare = ItemRarityID.Yellow;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");
			if (Item.damage > 0)
				tooltips.Add(new TooltipLine(Mod, "Damage", "Damage scales with progression"));
			line = new TooltipLine(Mod, "Slayer Item", "Slayer Item")
			{
				OverrideColor = Color.Red
			};
			tooltips.Add(line);
		}

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
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