using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusShrieker : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 36;

            Item.damage = 110;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3f;
            Item.crit = 10;
            Item.mana = 15;

            Item.useTime = 10;
            Item.useAnimation = 20;
            Item.reuseDelay = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item38;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 20f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 173000;
            Item.shoot = ModContent.ProjectileType<AreusShockwave>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(19)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.LeadBar, 3)
                .AddIngredient(ItemID.BeetleHusk, 7)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(15);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback,
                    player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}