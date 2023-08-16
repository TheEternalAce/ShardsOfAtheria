using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusRecursor : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 22;

            Item.damage = 33;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 6f;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 180000;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 12)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.SoulofLight, 8)
                .AddIngredient(ItemID.CrystalShard, 14)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        float rotationAngle = 30;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int NumProjectiles = 4 + Main.rand.Next(0, 4); // The number of projectiles that this gun will shoot.
            if (player.altFunctionUse == 2)
            {
                if (rotationAngle < 45)
                {
                    int addDamage = (int)rotationAngle / 2;
                    if (addDamage < 0)
                    {
                        addDamage *= -1;
                    }
                    damage += addDamage;
                }
            }
            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(rotationAngle));
                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                // Create a Projectile.
                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            if (player.altFunctionUse == 2)
            {
                rotationAngle = 30;
            }
            else
            {
                if (rotationAngle > -360)
                {
                    rotationAngle -= 10f;
                }
                else
                {
                    rotationAngle = -360;
                }
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}