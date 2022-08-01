using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Players;

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

			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ModContent.RarityType<SlayerRarity>();
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 45;
			Item.useAnimation = 45;
		}

        public override bool? UseItem(Player player)
        {
			if (!player.HasBuff(ModContent.BuffType<ClockCooldown>()))
			{
				player.Teleport(player.GetModPlayer<SlayerPlayer>().recentPos);
				player.statLife = player.GetModPlayer<SlayerPlayer>().recentLife;
				player.statMana = player.GetModPlayer<SlayerPlayer>().recentMana;
				//player.GetModPlayer<SoAPlayer>().areusResourceCurrent = player.GetModPlayer<SoAPlayer>().recentCharge;
				player.AddBuff(ModContent.BuffType<ClockCooldown>(), 5 * 60);
				player.GetModPlayer<SlayerPlayer>().saveTimer = 0;
				return true;
			}
			else return false;
		}

        public override void UpdateInventory(Player player)
        {
			player.GetModPlayer<SlayerPlayer>().spiderClock = true;
		}
    }
}