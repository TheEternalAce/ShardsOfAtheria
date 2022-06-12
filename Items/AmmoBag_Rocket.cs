using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
	public class AmmoBag_Rocket : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ammo Bag (Rocket)");
			Tooltip.SetDefault("Gives a stack of a random rocket type");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 22;
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 9999;
			Item.value = Item.buyPrice(0, 5);
		}

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
		{
			var ammoChooser = new WeightedRandom<int>();
			var source = player.GetSource_OpenItem(Type);

			ammoChooser.Add(ItemID.RocketI);
			ammoChooser.Add(ItemID.RocketII);
			ammoChooser.Add(ItemID.RocketIII);
			ammoChooser.Add(ItemID.RocketIV);
			ammoChooser.Add(ItemID.ClusterRocketI);
			ammoChooser.Add(ItemID.ClusterRocketII);
			ammoChooser.Add(ItemID.DryRocket);
			ammoChooser.Add(ItemID.WetRocket);
			ammoChooser.Add(ItemID.LavaRocket);
			ammoChooser.Add(ItemID.HoneyRocket);
			ammoChooser.Add(ItemID.MiniNukeI);
			ammoChooser.Add(ItemID.MiniNukeII);

			Main.LocalPlayer.QuickSpawnItem(source, ammoChooser, 999);
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AmmoBag>())
				.AddIngredient(ItemID.RocketI)
				.AddTile(TileID.WorkBenches)
				.Register();
        }
    }
}