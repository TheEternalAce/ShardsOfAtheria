using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using Terraria;
using Terraria.Audio;

namespace ShardsOfAtheria.Items
{
	public class SpiderClock: ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons Genesis, Atherial Time\n" +
				"Eventually");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Red;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 45;
			Item.useAnimation = 45;
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