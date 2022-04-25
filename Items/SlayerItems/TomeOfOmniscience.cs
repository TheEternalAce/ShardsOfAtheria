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

			if (player.HasBuff(ModContent.BuffType<BaseCombat>()))
				tooltips.Add(new TooltipLine(Mod, "CurrentKnowledgeBase", $"[c/FF0000:Combat]"));
			if (player.HasBuff(ModContent.BuffType<BaseConservation>()))
				tooltips.Add(new TooltipLine(Mod, "CurrentKnowledgeBase", $"[c/00FF00:Conservation]"));
			if (player.HasBuff(ModContent.BuffType<BaseExploration>()))
				tooltips.Add(new TooltipLine(Mod, "CurrentKnowledgeBase", $"[c/0000FF:Exploration]"));
		}
	}
}