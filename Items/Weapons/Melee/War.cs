using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ReLogic.Content;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class War : ModItem
	{
		public bool upgraded = false;
		public static Asset<Texture2D> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void OnCreate(ItemCreationContext context)
		{
			upgraded = false;
		}

		public override void SaveData(TagCompound tag)
		{
			tag["upgraded"] = upgraded;
		}

		public override void LoadData(TagCompound tag)
		{
			if (tag.ContainsKey("upgraded"))
			{
				upgraded = tag.GetBool("upgraded");
			}
		}

		public override void SetStaticDefaults()
		{
			if (!Main.dedServ)
			{
				glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
			}

			SacrificeTotal = 1;
			WeaponElements.Electric.Add(Type);
		}

		public override void SetDefaults()
		{
			Item.width = 62;
			Item.height = 62;

			Item.damage = 90;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 6;
			Item.crit = 6;
			Item.ArmorPenetration = 20;

			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;

			Item.shoot = ModContent.ProjectileType<Warframe>();
			Item.shootSpeed = 1;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(0, 2, 50);
		}

		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			if (upgraded)
			{
				damage += 0.12f;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.BreakerBlade)
				.AddIngredient(ItemID.BluePhasesaber)
				.AddIngredient(ItemID.BluePhasesaber)
				.AddTile(TileID.MythrilAnvil)
				.Register();

			CreateRecipe()
				.AddCondition(NetworkText.FromKey("Mods.ShardsOfAtheria.RecipeConditions.Upgrade"), r => false)
				.AddIngredient(Type)
				.AddIngredient(ItemID.HallowedBar, 20)
				.Register();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, upgraded ? 1 : 0);
			return false;
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (upgraded)
			{
				spriteBatch.Draw(glowmask.Value, position, null, drawColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
			}
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			if (upgraded)
			{
				Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value, Color.White, rotation, 1f);
			}
		}
	}
}