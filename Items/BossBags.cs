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
			var source = player.GetItemSource_OpenItem(arg);

			if (context == "bossBag" && arg == ItemID.EaterOfWorldsBossBag)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<OversizedWormsTooth>());
			}
			if (context == "bossBag" && arg == ItemID.QueenBeeBossBag)
			{
				if (Main.rand.NextFloat() < 0.5f)
				{
					player.QuickSpawnItem(source, ModContent.ItemType<HecateII>());
					player.QuickSpawnItem(source, ModContent.ItemType<DemonClaw>());
				}
				else
				{
					player.QuickSpawnItem(source, ModContent.ItemType<Glock80>());
					player.QuickSpawnItem(source, ModContent.ItemType<HiddenWristBlade>());
				}
				if (Main.rand.NextFloat() < .1f)
                {
					player.QuickSpawnItem(source, ModContent.ItemType<ShadowBrand>());
				}
			}
			if (context == "bossBag" && Main.hardMode && Main.rand.NextFloat() < .05f)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<AceOfSpades>());
				player.QuickSpawnItem(source, ModContent.ItemType<AcesGoldFoxMask>());
				player.QuickSpawnItem(source, ModContent.ItemType<AcesJacket>());
				player.QuickSpawnItem(source, ModContent.ItemType<AcesPants>());
			}
		}
	}
}
