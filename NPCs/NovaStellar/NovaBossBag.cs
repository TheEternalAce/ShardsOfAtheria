using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.NovaStellar
{
	public class NovaBossBag : ModItem
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Treasure Bag (Nova Stellar)");
			Tooltip.SetDefault("Right Click to open");
        }

        public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = Item.sellPrice(0,  7, silver: 50);
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
			var source = player.GetItemSource_OpenItem(Type);


			if (Main.rand.NextFloat() < .01f && Main.hardMode)
			{
				player.QuickSpawnItem(source, 3226);
				player.QuickSpawnItem(source, 3227);
				player.QuickSpawnItem(source, 3228);
				player.QuickSpawnItem(source, 3288);
			}
			player.QuickSpawnItem(source, ModContent.ItemType<GildedValkyrieWings>());
			player.QuickSpawnItem(source, ItemID.Feather, Main.rand.Next(10, 19));
			player.QuickSpawnItem(source, ItemID.GoldBar, Main.rand.Next(10, 19));
			player.QuickSpawnItem(source, dropChooser);
		}

		public override int BossBagNPC => ModContent.NPCType<NovaStellar>();
	}
}