using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BossSummons
{
    public class AreusSnakeScale : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Senterra, the Atherial Land\n" +
                "Eventually");
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; // This helps sort inventory know that this is a boss summoning Item.

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            if (!ModContent.GetInstance<ShardsConfigServerSide>().nonConsumeBoss)
            {
                Item.consumable = true;
            }

            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;

            Item.rare = ItemRarityID.Red;
        }
        /*
        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 5)
				.AddIngredient(ItemID.LunarBar, 5)
				.AddIngredient(ItemID.SpiderFang, 8)
				.AddIngredient(ItemID.GoldWatch)
				.AddTile(TileID.DemonAltar)
				.Register();
        }

        public override bool? UseItem(Player player)
		{
			SoundEngine.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		*/
    }
}