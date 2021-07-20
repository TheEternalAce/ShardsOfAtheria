using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Projectiles;
using Microsoft.Xna.Framework;

namespace SagesMania.Items.SlayerItems
{
	public class FlailOfFlesh : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.value = Item.sellPrice(silver: 5);
			item.rare = ItemRarityID.Expert;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 40;
			item.useTime = 40;
			item.knockBack = 4f;
			item.damage = 50;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<FlailOfFleshProj>();
			item.shootSpeed = 15.1f;
			item.UseSound = SoundID.Item1;
			item.summon = true;
			item.channel = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3; // 3 shots
			float rotation = MathHelper.ToRadians(5);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 5f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
    }
}