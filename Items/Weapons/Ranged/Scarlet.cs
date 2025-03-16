using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Projectiles.Ranged.GunRose;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Scarlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(7, 10);
            Item.AddElement(3);
            Item.AddRedemptionElement(9);
            Item.AddRedemptionElement(10);
            Item.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 22;

            Item.damage = 250;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3.75f;
            Item.crit = 8;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 13f;
            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = 42500;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -2);
        }

        public override void HoldItem(Player player)
        {
            player.scope = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HuntingRifle>())
                .AddIngredient(ModContent.ItemType<BrokenHeroGun>())
                .AddIngredient(ItemID.SniperRifle)
                .AddIngredient(ItemID.FragmentVortex, 20)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            return Main.rand.NextFloat() >= .66f;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet || type == ModContent.ProjectileType<BBProj>())
            {
                type = ProjectileID.MoonlordBullet;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(20);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                perturbedSpeed.Normalize();
                Projectile.NewProjectile(source, position, perturbedSpeed * 16f, ModContent.ProjectileType<ScarletRose>(), damage, knockback, player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}