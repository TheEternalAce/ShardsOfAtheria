using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DecaEquipment
{
    public class DecaShotgun : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Annihilator");
            Tooltip.SetDefault("'Shotgun of a godly machine'\n" +
              "'Makes things go boom'");
        }

        public override void SetDefaults()
        {
            item.damage = 200000;
            item.ranged = true;
            item.knockBack = 6f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.rare = ItemRarityID.Red;

            item.shoot = ItemID.PurificationPowder;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;

            item.noMelee = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item38;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 50;
            item.height = 20;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.Bullet)
                type = ProjectileID.ExplosiveBullet;
            int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(9));
                // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}