using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class DoubleBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 80;

            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;
            Item.crit = 5;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;

            Item.shootSpeed = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 2, 75);
            Item.shoot = ItemID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float falloff = Vector2.Distance(player.Center, Main.MouseWorld) * 0.002f;
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(10 * falloff);
            if (falloff <= 0.2f)
            {
                rotation = MathHelper.ToRadians(0);
            }
            position += Vector2.Normalize(velocity) * 10f;

            int newDamage = (int)(damage * (falloff * 5));
            if (newDamage < Item.damage)
            {
                newDamage = Item.damage;
            }
            else if (newDamage > 120)
            {
                newDamage = 120;
            }
            damage = newDamage;

            for (int i = 0; i < numberProjectiles; i++)
            {
                switch (i)
                {
                    case 0:
                        type = ProjectileID.CursedArrow;
                        break;
                    case 1:
                        type = ModContent.ProjectileType<LaserArrow>();
                        break;
                    case 2:
                        type = ProjectileID.IchorArrow;
                        break;
                }

                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                if (type == ProjectileID.CursedArrow)
                {
                    perturbedSpeed *= 2;
                }
                Projectile proj = Projectile.NewProjectileDirect(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
                proj.DamageType = DamageClass.Ranged;
                proj.extraUpdates = 1;
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}