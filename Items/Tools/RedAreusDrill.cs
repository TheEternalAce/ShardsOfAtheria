using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Tools;
using System.Collections.Generic;
using Terraria.Audio;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using ShardsOfAtheria.Utilities;

namespace ShardsOfAtheria.Items.Tools
{
	public class RedAreusDrill : AreusWeapon
	{
		public static Asset<Texture2D> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses highly concentrated electricity to cut through stones and ores\n" +
				"'Extremely dangerous... and fun!'");
			ItemID.Sets.IsDrill[Type] = true;

			if (!Main.dedServ)
			{
				glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
			}

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.width = 30;
			Item.height = 64;
			Item.useTime = 2; //Actual Break 1 = FAST 50 = SUPER SLOW
			Item.useAnimation = 10;
			Item.pick = 225;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.shoot = ModContent.ProjectileType<RedAreusDrillProj>();
			Item.shootSpeed = 32;

			if (ModContent.GetInstance<Config>().areusWeaponsCostMana)
				Item.mana = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusBarItem>(), 20)
				.AddIngredient(ItemID.LunarBar, 10)
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
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
			}
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value, new Color(255, 255, 255, 50) * 0.7f, rotation, scale);
        }
	}
}