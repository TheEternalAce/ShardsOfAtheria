using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    public class BlackHoleRepeater : SlayerItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Converts regular arrows into powerful Black Hole Bolts");
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = Item.useAnimation = 18;
            Item.width = 18;
            Item.height = 18;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 180;
            Item.crit = 8;
            Item.knockBack = 2f;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(5))
            {
                const int NumProjectiles = 4; // The number of projectiles that this gun will shoot.

                for (int i = 0; i < NumProjectiles; i++)
                {
                    // Rotate the velocity randomly by 30 degrees at max.
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                    // Decrease velocity randomly for nicer visuals.
                    newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                    // Create a Projectile.
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
            }

            return true; // Return false because we don't want tModLoader to shoot projectile
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<BlackHoleBolt>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.FragmentVortex, 32)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}