using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Players;
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
			base.SetStaticDefaults();
		}

        public override void SetDefaults()
		{
			Item.width = 15;
			Item.height = 22;
			Item.accessory = true;

			Item.rare = ModContent.RarityType<SlayerRarity>();
			Item.value = Item.sellPrice(0, 1);
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SlayerPlayer>().omnicientTome = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			SlayerPlayer slayer = Main.LocalPlayer.GetModPlayer<SlayerPlayer>();
			var list = ShardsOfAtheria.TomeKey.GetAssignedKeys();
			string keyname = "Not bound";

			if (list.Count > 0)
			{
				keyname = list[0];
			}

			tooltips.Add(new TooltipLine(Mod, "CycleKnowledgeBase", $"Press '[i:{keyname}]' to cycle between 3 Knowledge Bases:\n" +
				"Combat, Conservation and Exploration"));

			var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");

			if (slayer.TomeKnowledge == 0)
				line = new TooltipLine(Mod, "CurrentKnowledgeBase", "Combat")
				{
					OverrideColor = Color.Red
				};
			if (slayer.TomeKnowledge == 1)
				line = new TooltipLine(Mod, "CurrentKnowledgeBase", "Conservation")
				{
					OverrideColor = Color.Green
				};
			if (slayer.TomeKnowledge == 2)
				line = new TooltipLine(Mod, "CurrentKnowledgeBase", "Exploration")
				{
					OverrideColor = Color.Blue
				};

			tooltips.Add(line);
			base.ModifyTooltips(tooltips);
		}
	}
}