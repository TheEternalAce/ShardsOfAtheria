using SagesMania.Items.Accessories;
using SagesMania.Items.Placeable;
using SagesMania.Items.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.NPCs.NovaSkyloft
{
	public class NovaBossBag : ModItem
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("Right Click to open");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = Item.sellPrice(gold: 7, silver: 50);
			item.rare = ItemRarityID.Expert;
			item.maxStack = 999;
			item.expert = true;
			item.consumable = true;
		}

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
		{
			var dropChooser = new WeightedRandom<int>();
			dropChooser.Add(ModContent.ItemType<ValkyrieCrown>());
			dropChooser.Add(ModContent.ItemType<ValkyrieBlade>());
			int choice = dropChooser;

			if (Main.rand.NextFloat() < .01f && Main.hardMode)
			{
				player.QuickSpawnItem(3226);
				player.QuickSpawnItem(3227);
				player.QuickSpawnItem(3228);
				player.QuickSpawnItem(3288);
			}
			player.QuickSpawnItem(ModContent.ItemType<GildedValkyrieWings>());
			player.QuickSpawnItem(ItemID.Feather, Main.rand.Next(10, 19));
			player.QuickSpawnItem(ItemID.GoldBar, Main.rand.Next(10, 19));
			player.QuickSpawnItem(choice);
			player.QuickSpawnItem(ModContent.ItemType<PhaseOreItem>(), 7);
		}

		public override int BossBagNPC => ModContent.NPCType<ValkyrieNova>();
	}
}