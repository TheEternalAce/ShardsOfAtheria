using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace SagesMania.Items.DecaEquipment
{
    public class DecaRifle : DecaEquipment
    {
        private int shootingSoundsTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Stormer");
            Tooltip.SetDefault("Fires a storm of powerful luminite bullets\n" +
              "66% chance to not consume ammo\n" +
              "'Rifle of a godly machine'");
        }

        public override void SetDefaults()
        {
            item.damage = 200000;
            item.ranged = true;
            item.knockBack = 6f;
            item.useTime = 1;
            item.useAnimation = 50;
            item.rare = ItemRarityID.Red;

            item.shoot = ItemID.PurificationPowder;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;

            item.noMelee = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.reuseDelay = 25;
            item.width = 50;
            item.height = 20;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        
		public override bool ConsumeAmmo(Player player)
		{
			return !(player.itemAnimation < item.useAnimation - 2) || Main.rand.NextFloat() >= .66f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            shootingSoundsTimer++;
            if (shootingSoundsTimer == 1)
            {
                Main.PlaySound(item.UseSound);
                shootingSoundsTimer = 0;
            }
            if (type == ProjectileID.Bullet)
                type = ProjectileID.MoonlordBullet;
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
    }
}