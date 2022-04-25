using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class BloodScythe : SlayerItem
	{
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");
			line = new TooltipLine(Mod, "BloodScythe", "...")
			{
				OverrideColor = Color.DarkGray
			};
			tooltips.Add(line);
		}

		public override void SetDefaults()
		{
			Item.damage = 190;
			Item.DamageType = DamageClass.Melee;
			Item.width = 70;
			Item.height = 64;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = false;
			Item.useTurn = true;
			Item.crit = 100;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.WitheredArmor, 600);
			target.AddBuff(BuffID.WitheredWeapon, 600);
			target.AddBuff(BuffID.Ichor, 600);
			target.AddBuff(BuffID.Bleeding, 600);
			target.AddBuff(BuffID.Daybreak, 600);
		}
	}
}