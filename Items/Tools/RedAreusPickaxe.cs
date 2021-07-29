using SagesMania.Items.Placeable;
using SagesMania.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;

namespace SagesMania.Items.Tools
{
	public class RedAreusPickaxe : AreusWeapon
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses highly concentrated electricity to cut through stones and ores");
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
			item.useAnimation = 10;
			item.pick = 150;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = Item.sellPrice(gold: 5);
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item15;
			item.autoReuse = true;
			item.useTurn = true;

			if (!Config.areusWeaponsCostMana)
				areusResourceCost = 1;
			else item.mana = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AreusBarItem>(), 20);
			recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 13);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(10))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric);
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
			}
		}
	}
}