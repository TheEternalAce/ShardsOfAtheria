using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;

namespace SagesMania.Items.DecaEquipment
{
    public class DecaBow : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Bow");
            Tooltip.SetDefault("Rains powerful luminite arrows\n" +
              "66% chance to not consume ammo\n" +
              "'Bow of a godly machine'");
        }

        public override void SetDefaults()
        {
            item.damage = 200000;
            item.ranged = true;
            item.knockBack = 6f;
            item.useTime = 5;
            item.useAnimation = 20;
            item.rare = ItemRarityID.Red;

            item.shoot = ItemID.PurificationPowder;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Arrow;

            item.noMelee = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.reuseDelay = 25;
            item.width = 50;
            item.height = 20;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 4);
        }

        public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .66f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ProjectileID.MoonlordArrow;
            Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            for (int i = 0; i < 3; i++)
            {
                position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
                position.Y -= (100 * i);
                Vector2 heading = target - position;
                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }
                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }
                heading.Normalize();
                heading *= new Vector2(speedX, speedY).Length();
                speedX = heading.X;
                speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI, 0f, ceilingLimit);
            }
            return false;
        }
    }
}