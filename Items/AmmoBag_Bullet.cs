using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
	public class AmmoBag_Bullet : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ammo Bag (Bullet)");
			Tooltip.SetDefault("Gives a stack of a random bullet type");
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

			ammoChooser.Add(ItemID.MusketBall);
			ammoChooser.Add(ItemID.MeteorShot);
			ammoChooser.Add(ItemID.SilverBullet);
			ammoChooser.Add(ItemID.CrystalBullet);
			ammoChooser.Add(ItemID.CursedBullet);
			ammoChooser.Add(ItemID.ChlorophyteBullet);
			ammoChooser.Add(ItemID.HighVelocityBullet);
			ammoChooser.Add(ItemID.IchorBullet);
			ammoChooser.Add(ItemID.VenomBullet);
			ammoChooser.Add(ItemID.PartyBullet);
			ammoChooser.Add(ItemID.NanoBullet);
			ammoChooser.Add(ItemID.ExplodingBullet);
			ammoChooser.Add(ItemID.GoldenBullet);
			ammoChooser.Add(ItemID.MoonlordBullet);
			ammoChooser.Add(ItemID.TungstenBullet);

			Main.LocalPlayer.QuickSpawnItem(source, ammoChooser, 999);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AmmoBag>())
				.AddIngredient(ItemID.MusketBall)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}