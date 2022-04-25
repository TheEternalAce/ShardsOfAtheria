using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class TomeOfOmniscience : SlayerItem
	{
        public override void SetStaticDefaults()
		{
		}

        public override void SetDefaults()
		{
			Item.width = 15;
			Item.height = 22;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SoAPlayer>().omnicientTome = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			Player player = Main.LocalPlayer;
			var list = ShardsOfAtheria.TomeKey.GetAssignedKeys();
			string keyname = "Not bound";

			if (list.Count > 0)
			{
				keyname = list[0];
			}

			tooltips.Add(new TooltipLine(Mod, "CycleKnowledgeBase", $"Press '[i:{keyname}]' to cycle between 3 Knowledge Bases:\n" +
				"Combat, Conservation and Exploration"));

			var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");

			if (player.HasBuff(ModContent.BuffType<BaseCombat>()))
				line = new TooltipLine(Mod, "CurrentKnowledgeBase", "Combat")
				{
					OverrideColor = Color.Red
				};
			if (player.HasBuff(ModContent.BuffType<BaseConservation>()))
				line = new TooltipLine(Mod, "CurrentKnowledgeBase", "Conservation")
				{
					OverrideColor = Color.Green
				};
			if (player.HasBuff(ModContent.BuffType<BaseExploration>()))
				line = new TooltipLine(Mod, "CurrentKnowledgeBase", "Exploration")
				{
					OverrideColor = Color.Blue
				};

			tooltips.Add(line);
		}
	}
}