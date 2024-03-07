using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusCalibratedShotgun : ModItem
    {
        int calibrationMode = 0;
        float speedMultiplier = 1f;

        public override void SetStaticDefaults()
        {
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
            Item.rare = ItemDefaults.RarityEarlyHardmode;
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

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                calibrationMode++;
                if (calibrationMode > 2)
                {
                    calibrationMode = 0;
                }
                var key = this.GetLocalizationKey("Calibration" + calibrationMode + ".DisplayName");
                string calibration = Language.GetTextValue(key);
                CombatText.NewText(player.Hitbox, Color.Cyan, calibration);
                type = 0;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var shards = player.Shards();
            bool overdrive = shards.Overdrive;
            int numberProjectiles = 2 + Main.rand.Next(0, 3);
            float rotation = MathHelper.ToRadians(20);
            if (calibrationMode == 1)
            {
                int misfireDemominator = 5;
                numberProjectiles *= 2;
                if (overdrive)
                {
                    numberProjectiles *= 2;
                    misfireDemominator = 3;
                }
                if (Main.rand.NextBool(misfireDemominator))
                {
                    return false;
                }
            }
            if (calibrationMode == 2)
            {
                rotation = MathHelper.ToRadians(5);
                numberProjectiles = 2;
                if (overdrive)
                {
                    rotation = 0;
                }
            }
            if (numberProjectiles > 1)
            {
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(rotation);
                    newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                    Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback);
                }
            }
            return calibrationMode == 2 && !overdrive;
        }

        public override float UseSpeedMultiplier(Player player)
        {
            return speedMultiplier;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            var shards = player.Shards();
            bool overdrive = shards.Overdrive;
            if (calibrationMode == 2)
            {
                damage *= 3.5f;
                if (overdrive)
                {
                    damage *= 1.5f;
                }
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var key = this.GetLocalizationKey("Calibration" + calibrationMode + ".Tooltip");
            string lineText = Language.GetTextValue(key);
            TooltipLine line = new(Mod, "", lineText);
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
        }

        public override void UpdateInventory(Player player)
        {
            string name = Language.GetTextValue(this.GetLocalizationKey("DisplayName"));
            var key = this.GetLocalizationKey("Calibration" + calibrationMode + ".DisplayName");
            string mode = Language.GetTextValue(key);
            Item.SetNameOverride(name + mode);

            var shards = player.Shards();
            bool overdrive = shards.Overdrive;
            speedMultiplier = 1f;
            if (calibrationMode == 1)
            {
                speedMultiplier = 0.5f;
            }
            if (calibrationMode == 2)
            {
                speedMultiplier = 0.33f;
            }
            if (calibrationMode > 0)
            {
                if (overdrive)
                {
                    speedMultiplier -= 0.1f;
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4, 0);
        }
    }
}