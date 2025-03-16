using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic.ElecGauntlet;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            Item.AddAreus();
            Item.AddDamageType(5);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;

            Item.damage = 117;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 5;
            Item.crit = 2;
            Item.mana = 12;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1;
            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = 50000;
            Item.shoot = ModContent.ProjectileType<ElecBlade>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot++;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = null;
                Item.shootSpeed = 1;
                if (Item.shoot > ModContent.ProjectileType<ElecPistol>())
                {
                    Item.shoot = ModContent.ProjectileType<ElecBlade>();
                    Item.UseSound = SoundID.Item1;
                }
                if (Item.shoot == ModContent.ProjectileType<ElecDagger>())
                {
                    Item.useStyle = ItemUseStyleID.Swing;
                    Item.UseSound = SoundID.Item1;
                }
                if (Item.shoot == ModContent.ProjectileType<ElecKnuckle>())
                {
                    Item.useStyle = ItemUseStyleID.Rapier;
                    Item.UseSound = SoundID.Item1;
                    Item.shootSpeed = 2;
                }
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<ElecDagger>())
            {
                float numberProjectiles = 3; // 3 shots
                float rotation = MathHelper.ToRadians(10);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    perturbedSpeed.Normalize();
                    Projectile.NewProjectile(source, position, perturbedSpeed * 10f, type, damage, knockback, player.whoAmI);
                }
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }

    public class ElecGauntletPlayer : ModPlayer
    {
        /// <summary>
        /// [0,i] = Projectile type
        /// [1,i] = Freshness multiplier
        /// [2,i] = First use
        /// </summary>
        private readonly object[,] Projs =
        {
            { ModContent.ProjectileType<AreusArrowProj_Gauntlet>(), 1.0f, false },
            { ModContent.ProjectileType<AreusBulletProj_Gauntlet>(), 1.0f, false },
            { ModContent.ProjectileType<ElecBlade>(), 1.0f, false },
            { ModContent.ProjectileType<ElecDagger>(), 1.0f, false },
            { ModContent.ProjectileType<ElecKnuckle>(), 1.0f, false },
            { ModContent.ProjectileType<FireCannon_Fire_Gauntlet>(), 1.0f, false },
            { ModContent.ProjectileType<LightningBolt_Gauntlet>(), 1.0f, false },
        };
        private int resetTimer = 0;

        public override void ResetEffects()
        {
            if (resetTimer > 0)
            {
                resetTimer--;
            }
            int used = UsedCount();
            if (used >= 6)
            {

            }
            for (int i = 0; i < 7; i++)
            {
                if (resetTimer == 0 || used >= 6)
                {
                    Projs[i, 1] = 1.0f;
                    Projs[i, 2] = false;
                }
                float minMult = 0.5f;
                if ((float)Projs[i, 1] < minMult)
                {
                    Projs[i, 1] = minMult;
                }
            }
        }

        int UsedCount()
        {
            int count = 0;
            for (int i = 0; i < 7; i++)
            {
                if ((bool)Projs[i, 2])
                {
                    count++;
                }
            }
            return count;
        }

        public void AddType(int type)
        {
            for (int i = 0; i < 7; i++)
            {
                if ((int)Projs[i, 0] == type)
                {
                    if (UsedCount() < 6)
                    {
                        resetTimer = 180;
                    }
                    if (!(bool)Projs[i, 2])
                    {
                        Projs[i, 2] = true;
                    }
                    break;
                }
            }
        }

        public void ModifyGauntletHit(ref NPC.HitModifiers modifiers, int type)
        {
            for (int i = 0; i < 7; i++)
            {
                if ((int)Projs[i, 0] == type)
                {
                    if ((bool)Projs[i, 2])
                    {
                        int used = UsedCount();
                        modifiers.FinalDamage += used / 100f * 4f;
                        modifiers.FinalDamage *= (float)Projs[i, 1];
                        Projs[i, 1] = (float)Projs[i, 1] - 0.1f;
                    }
                    break;
                }
            }
        }
    }
}