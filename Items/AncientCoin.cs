using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items
{
	public class AncientCoin: ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons Death\n" +
				"Eventually\n" +
				"[c/323232:''... Where did you get that..?'']\n" +
				"[c/323232:''Oh, it's a fake..'']\n" +
				"[c/323232:''I suppose I'll accept your challenge..'']\n" +
				"[c/323232:''Oh wait, I can't yet.'']");
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
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(ItemID.DeathSickle);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool UseItem(Player player)
		{
			Main.NewText("[c/323232:So, you wanna fight me?]");
			Main.NewText("[c/323232:Not right now]");
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
	}
}