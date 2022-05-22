using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

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
			Item.width = 60;
			Item.height = 54;
			Item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
			Item.useAnimation = 10;
			Item.pick = 150;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(0,  5);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
			Item.useTurn = true;

			if (ModContent.GetInstance<ConfigServerSide>().areusWeaponsCostMana)
				Item.mana = 0;
			else chargeCost = 0;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 20)
				.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 13)
				.AddIngredient(ItemID.Wire, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override void RightClick(Player player)
		{
			int areusChargePackIndex = Main.LocalPlayer.FindItem(ModContent.ItemType<AreusChargePack>());
			Main.LocalPlayer.inventory[areusChargePackIndex].stack--;
			areusCharge += 500;
			SoundEngine.PlaySound(SoundID.NPCHit53);
			CombatText.NewText(player.Hitbox, Color.Aqua, 50);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(10))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric);
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch);
			}
		}
	}
}