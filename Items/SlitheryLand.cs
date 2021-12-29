using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using Terraria;

namespace ShardsOfAtheria.Items
{
	public class SlitheryLand: ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons Senterra, the Atherial Land\n" +
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
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 5)
				.AddIngredient(ItemID.LunarBar, 5)
				.AddIngredient(ItemID.DirtBlock, 20)
				.AddIngredient(ItemID.StoneBlock, 20)
				.AddIngredient(ItemID.Cloud, 20)
				.AddTile(TileID.DemonAltar)
				.Register();
		}
		
		public override bool UseItem(Player player)
		{
			SoundEngine.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		*/
	}
}