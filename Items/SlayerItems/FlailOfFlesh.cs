using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Summon;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class FlailOfFlesh : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Throws 3-6 Hungry\n" +
                "'Your very own Hungry as a pet! Adorable..?'");
        }

        public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 38;
			Item.value = Item.sellPrice(silver: 5);
			Item.rare = ModContent.RarityType<SlayerRarity>();
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.knockBack = 4f;
			Item.damage = 50;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<FlailOfFleshProj>();
			Item.shootSpeed = 20f;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Summon;
			Item.channel = true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 3 + Main.rand.Next(0, 3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(20);
			position += Vector2.Normalize(velocity) * 20f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 Projectile.
				Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			}
			return false; // return false to stop vanilla from calling Projectile.NewProjectile.
		}
    }
}