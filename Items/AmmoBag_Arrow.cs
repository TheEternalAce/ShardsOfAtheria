using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
	public class AmmoBag_Arrow : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ammo Bag (Arrow)");
			Tooltip.SetDefault("Gives a stack of a random arrow type");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 22;
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 10;
		}

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
		{
			var ammoChooser = new WeightedRandom<int>();
			var source = player.GetSource_OpenItem(Type);

			ammoChooser.Add(ItemID.BoneArrow);
			ammoChooser.Add(ItemID.ChlorophyteArrow);
			ammoChooser.Add(ItemID.CursedArrow);
			ammoChooser.Add(ItemID.FlamingArrow);
			ammoChooser.Add(ItemID.FrostburnArrow);
			ammoChooser.Add(ItemID.HellfireArrow);
			ammoChooser.Add(ItemID.HolyArrow);
			ammoChooser.Add(ItemID.IchorArrow);
			ammoChooser.Add(ItemID.JestersArrow);
			ammoChooser.Add(ItemID.MoonlordArrow);
			ammoChooser.Add(ItemID.UnholyArrow);
			ammoChooser.Add(ItemID.VenomArrow);
			ammoChooser.Add(ItemID.WoodenArrow);

			Main.LocalPlayer.QuickSpawnItem(source, ammoChooser, 999);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AmmoBag>())
				.AddIngredient(ItemID.WoodenArrow)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}