using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Weapons;
using SagesMania.Items.Accessories;

namespace SagesMania.Items
{
    public class BossBags : GlobalItem
	{
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == ItemID.EaterOfWorldsBossBag)
			{
				player.QuickSpawnItem(ModContent.ItemType<OversizedWormsTooth>());
			}
			if (context == "bossBag" && arg == ItemID.BrainOfCthulhuBossBag)
			{
				player.QuickSpawnItem(ModContent.ItemType<TomeOfOmniscience>());
			}
			if (context == "bossBag" && arg == ItemID.EyeOfCthulhuBossBag)
			{
				player.QuickSpawnItem(ModContent.ItemType<Cataracnia>());
			}
		}
	}
}
