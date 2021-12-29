using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class SpidersMechanicalClock : SlayerItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Spider's Mechanical Clock");
			Tooltip.SetDefault("Saves your current position after 5 seconds, use to teleport to saved position\n" +
				"All stats you had in that save are regained\n" +
				"There's an engraving on the back\n" +
				"'-A gift to you, my beloved little sister <3'\n" +
				"Must be used in hotbar");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Expert;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 45;
			Item.useAnimation = 45;
		}

        public override bool? UseItem(Player player)
        {
			if (!player.HasBuff(ModContent.BuffType<ClockCooldown>()))
			{
				player.Teleport(player.GetModPlayer<SMPlayer>().recentPos);
				player.statLife = player.GetModPlayer<SMPlayer>().recentLife;
				player.statMana = player.GetModPlayer<SMPlayer>().recentMana;
				player.GetModPlayer<SMPlayer>().areusResourceCurrent = player.GetModPlayer<SMPlayer>().recentCharge;
				player.AddBuff(ModContent.BuffType<ClockCooldown>(), 5 * 60);
				player.GetModPlayer<SMPlayer>().saveTimer = 0;
				return true;
			}
			else return false;
		}

        public override void UpdateInventory(Player player)
        {
			player.GetModPlayer<SMPlayer>().spiderClock = true;
		}
    }
}