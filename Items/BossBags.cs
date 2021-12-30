using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Weapons;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.DevItems.AceOfSpades2370;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;

namespace ShardsOfAtheria.Items
{
    public class BossBags : GlobalItem
	{
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == ItemID.EyeOfCthulhuBossBag)
			{
				player.QuickSpawnItem(ModContent.ItemType<Cataracnia>());
			}
			if (context == "bossBag" && arg == ItemID.EaterOfWorldsBossBag)
			{
				player.QuickSpawnItem(ModContent.ItemType<OversizedWormsTooth>());
			}
			if (context == "bossBag" && arg == ItemID.BrainOfCthulhuBossBag)
			{
				player.QuickSpawnItem(ModContent.ItemType<TomeOfOmniscience>());
			}
			if (context == "bossBag" && arg == ItemID.QueenBeeBossBag)
			{
				if (Main.rand.NextFloat() < 0.5f)
				{
					player.QuickSpawnItem(ModContent.ItemType<HecateII>());
					player.QuickSpawnItem(ModContent.ItemType<DemonClaw>());
				}
				else
				{
					player.QuickSpawnItem(ModContent.ItemType<Glock80>());
					player.QuickSpawnItem(ModContent.ItemType<HiddenWristBlade>());
				}
				if (Main.rand.NextFloat() < .1f)
                {
					if (Main.rand.NextFloat() < .5f)
						player.QuickSpawnItem(ModContent.ItemType<MarkOfAnastasia>());
					else player.QuickSpawnItem(ModContent.ItemType<ShadowBrand>());
				}
			}
			if (context == "bossBag" && Main.hardMode && Main.rand.NextFloat() < .05f)
			{
				player.QuickSpawnItem(ModContent.ItemType<AceOfSpades>());
				player.QuickSpawnItem(ModContent.ItemType<AcesGoldFoxMask>());
				player.QuickSpawnItem(ModContent.ItemType<AcesJacket>());
				player.QuickSpawnItem(ModContent.ItemType<AcesPants>());
			}
		}
	}
}
