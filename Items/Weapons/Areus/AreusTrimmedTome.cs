using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusTrimmedTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;

            Item.damage = 50;
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
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 7)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 4; // 4 shots
            float rotation = MathHelper.ToRadians(45);
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