using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class SilverSpear : ModItem
    {
        const int COMBO_THRESHOLD = 15;
        const int COMBO_START = 65;

        int combo = 0;
        int comboTimer = 0;

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(11, 1);
            Item.AddRedemptionElement(1);
        }

        public override void SetDefaults()
        {
            Item.width = 94;
            Item.height = 96;

            Item.damage = 90;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityPlantera;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<SilverSpearSwing>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void HoldItem(Player player)
        {
            comboTimer--;
            if (comboTimer == COMBO_THRESHOLD) SoundEngine.PlaySound(SoA.SilverRingsSoft);
            if (comboTimer <= 0) comboTimer = COMBO_START;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem.type != Type) comboTimer = COMBO_START;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (comboTimer <= COMBO_THRESHOLD && comboTimer > 0) damage = (int)(damage * 1.75f);
            if (combo++ == 2)
            {
                type = ModContent.ProjectileType<SilverSpearThrust>();
                combo = 0;
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float ai = 0f;
            if (comboTimer <= COMBO_THRESHOLD && comboTimer > 0) ai = 1f;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, ai);
            comboTimer = COMBO_START;
            return false;
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }

        public static void ShootRings(Projectile projectile, Vector2 angleVector)
        {
            float numberProjectiles = 4;
            float shardRotation = MathHelper.ToRadians(20f);
            Vector2 position = projectile.Center - angleVector * 40f;
            Vector2 velocity = angleVector * 12f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-shardRotation, shardRotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(projectile.GetSource_FromThis(), position, perturbedSpeed, ModContent.ProjectileType<SilverRing>(), (int)(projectile.damage * 0.75f), 0f);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverBar, 20)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.Ectoplasm, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TungstenBar, 20)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.Ectoplasm, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}