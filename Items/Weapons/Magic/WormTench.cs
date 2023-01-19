using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class WormTench : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 26;

            Item.damage = 17;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;
            Item.crit = 5;
            Item.mana = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item17;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 1, 25);
            Item.shoot = ModContent.ProjectileType<VileShot>();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(3, 1);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int NumProjectiles = 4 + Main.rand.Next(0, 4); // The number of projectiles that this gun will shoot.

            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                // Create a Projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false; // Return false because we don't want tModLoader to shoot projectile
        }
    }
}
