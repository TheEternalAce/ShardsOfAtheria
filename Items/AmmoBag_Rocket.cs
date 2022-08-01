using Terraria;
using Terraria.GameContent.Creative;
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

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 22;
			Item.maxStack = 9999;

			Item.rare = ItemRarityID.Blue;
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

			Main.LocalPlayer.QuickSpawnItem(source, ammoChooser, 9999);
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AmmoBag>())
				.AddIngredient(ItemID.RocketI, 100)
				.AddTile(TileID.WorkBenches)
				.Register();
        }
    }
}