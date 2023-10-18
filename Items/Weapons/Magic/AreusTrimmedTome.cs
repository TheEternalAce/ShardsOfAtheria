using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusTrimmedTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;

            Item.damage = 34;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3.75f;
            Item.crit = 16;
            Item.mana = 10;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 1, 75);
            Item.shoot = ModContent.ProjectileType<ElectricSpike>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddIngredient(ItemID.GoldBar, 3)
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 7)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3; // 3 shots
            float rotation = MathHelper.PiOver4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 vel = Vector2.Normalize(player.Center - Main.MouseWorld);
                Vector2 perturbedSpeed = vel.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 8f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}