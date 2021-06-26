using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class Scarlet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Converts regular bullets into Chlorophyte Bullets\n" +
				"66% chance to not consume ammo\n" +
				"[c/960096:''Now we're talkin'!'']");
		}

		public override void SetDefaults()
		{
			item.damage = 500;
			item.ranged = true;
			item.noMelee = true;
			item.width = 56;
			item.height = 18;
			item.useTime = 90;
			item.useAnimation = 90;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Green;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/BBGunShoot");
			item.autoReuse = true;
			item.crit = 20;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 13f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 2);
		}

        public override void HoldItem(Player player)
        {
			player.scope = true;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BBGun>());
			recipe.AddIngredient(ModContent.ItemType<BrokenHeroGun>());
			recipe.AddIngredient(ItemID.SniperRifle);
			recipe.AddIngredient(ItemID.FragmentVortex, 20);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .66f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.Bullet || type == ModContent.ProjectileType<BBProjectile>())
			{
				type = ProjectileID.ChlorophyteBullet;
			}
			return true;
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/Weapons/Scarlet_Glow");
			spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale,
				SpriteEffects.None,
				0f
			);
		}
	}
}