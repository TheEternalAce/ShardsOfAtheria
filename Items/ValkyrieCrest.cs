using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Placeable;
using SagesMania.Tiles;

namespace SagesMania.Items
{
	public class ValkyrieCrest : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Summons a holy Harpy Knight\n" +
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
			recipe.AddRecipeGroup("SM:GoldBars", 10);
			recipe.AddIngredient(ItemID.Feather, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool UseItem(Player player)
		{
			Main.NewText("So, you wanna fight a bird knigt?");
			Main.NewText("Not right now");
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
	}
}