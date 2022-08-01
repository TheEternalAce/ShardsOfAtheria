using Terraria;
using Terraria.GameContent.Creative;
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

			ammoChooser.Add(ItemID.MusketBall);
			ammoChooser.Add(ItemID.SilverBullet);
			ammoChooser.Add(ItemID.TungstenBullet);

			if (NPC.downedBoss2)
			{
				ammoChooser.Add(ItemID.MeteorShot);
			}

			if (Main.hardMode)
			{
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
			}
			if (NPC.downedMoonlord)
			{
				ammoChooser.Add(ItemID.MoonlordBullet);
			}

			Main.LocalPlayer.QuickSpawnItem(source, ammoChooser, 9999);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AmmoBag>())
				.AddIngredient(ItemID.MusketBall, 100)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
