using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
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
            Item.damage = 200000;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 6f;
            Item.useTime = 5;
            Item.useAnimation = 20;

            Item.shoot = ItemID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;

            Item.noMelee = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.reuseDelay = 25;
            Item.width = 34;
            Item.height = 56;
        }

        public override Vector2? HoldoutOffset()
        {
            //return new Vector2(-20, 4);
            return new Vector2(0, 0);
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            return Main.rand.NextFloat() >= .66f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            // Loop these functions 3 times.
            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
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
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ProjectileID.MoonlordArrow;
        }
    }
}