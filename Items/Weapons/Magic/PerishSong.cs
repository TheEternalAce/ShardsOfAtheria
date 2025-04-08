using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class PerishSong : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(6, 12);
            Item.AddElement(1);
            Item.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 20;

            Item.damage = 70;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4;
            Item.crit = 20;
            Item.mana = 12;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item103;
            Item.autoReuse = true;

            Item.shootSpeed = 10;
            Item.rare = ItemDefaults.RaritySlayer;
            Item.shoot = ModContent.ProjectileType<DeathNote>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.FragmentNebula, 32)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3; // 3 shots
            float rotation = MathHelper.ToRadians(10);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}