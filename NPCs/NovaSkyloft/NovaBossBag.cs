using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.NovaSkyloft
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
			Item.width = 30;
			Item.height = 20;
			Item.value = Item.sellPrice(gold: 7, silver: 50);
			Item.rare = ItemRarityID.Expert;
			Item.maxStack = 999;
			Item.expert = true;
			Item.consumable = true;
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