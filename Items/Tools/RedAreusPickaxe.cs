using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Tools
{
	public class RedAreusPickaxe : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses highly concentrated electricity to cut through stones and ores");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
			Item.useAnimation = 10;
			Item.pick = 150;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
			Item.useTurn = true;

			if (!Config.areusWeaponsCostMana)
				areusResourceCost = 1;
			else Item.mana = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 20)
				.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 13)
				.AddIngredient(ItemID.Wire, 10)
				.AddTile(ModContent.TileType<CobaltWorkbench>())
				.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(10))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric);
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
			}
		}
	}
}