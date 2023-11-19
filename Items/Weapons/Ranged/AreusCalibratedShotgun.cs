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
        const int CALIBRATION_MAX = 2;
        int calibration = 0;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
            Item.AddElementElec();
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                calibration++;
                if (calibration > CALIBRATION_MAX)
                {
                    calibration = 0;
                }
                var key = this.GetLocalizationKey("Calibration" + calibration + ".DisplayName");
                string cal = Language.GetTextValue(key);
                CombatText.NewText(player.Hitbox, Color.Cyan, cal);
                return false;
            }
            int numberProjectiles = 4 + Main.rand.Next(0, 4);
            float rotation = MathHelper.ToRadians(30);
            if (calibration == 1)
            {
                numberProjectiles *= 2;
                if (Main.rand.NextBool(5))
                {
                    return false;
                }
            }
            if (calibration == 2)
            {
                rotation = MathHelper.ToRadians(5);
                numberProjectiles = 3;
            }
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(rotation);
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override float UseSpeedMultiplier(Player player)
        {
            if (calibration == 1)
            {
                return 0.5f;
            }
            if (calibration == 2)
            {
                return 0.33f;
            }
            return base.UseSpeedMultiplier(player);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (calibration == 2)
            {
                damage *= 2f;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var key = this.GetLocalizationKey("Calibration" + calibration + ".DisplayName");
            string mode = Language.GetTextValue(key);
            tooltips[0].Text += mode;

            var key1 = this.GetLocalizationKey("Calibration" + calibration + ".Tooltip");
            string lineText = Language.GetTextValue(key1);
            TooltipLine line = new(Mod, "", lineText);
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4, 0);
        }
    }
}