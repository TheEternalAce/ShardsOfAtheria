using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using Terraria;

namespace SagesMania.Items
{
	public class SpiderClock: ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons Atherial Time\n" +
				"Eventually");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Red;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 45;
			item.useAnimation = 45;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 5);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(ItemID.SpiderFang, 8);
			recipe.AddIngredient(ItemID.GoldWatch);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
		{
			Main.NewText("So, you wanna fight a big spider?");
			Main.NewText("Not right now");
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
    }
}