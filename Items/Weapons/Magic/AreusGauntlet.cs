using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.ModCondition;
using ShardsOfAtheria.Projectiles.Weapon.Magic.ElecGauntlet;
using System.Collections.Generic;
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
            Item.ResearchUnlockCount = 1;
            Item.AddElec();
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
            Item.rare = ItemRarityID.Cyan;
            Item.value = 50000;
            Item.shoot = ModContent.ProjectileType<ElecBlade>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusGambit>())
                .AddIngredient(ModContent.ItemType<AreusSword>())
                .AddIngredient(ModContent.ItemType<AreusDagger>())
                .AddIngredient(ModContent.ItemType<AreusMagnum>())
                .AddIngredient(ModContent.ItemType<AreusRailgun>())
                .AddIngredient(ModContent.ItemType<AreusBow>())
                .AddIngredient(ItemID.BeetleHusk, 15)
                .AddCondition(SoAConditions.Upgrade)
                .Register();
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
            var gPlayer = player.GetModPlayer<ElecGauntletPlayer>();
            if (!gPlayer.UsedProjs.Contains(type))
            {
                gPlayer.UsedProjs.Add(type);
            }
            else
            {
                gPlayer.freshMultiplier -= 0.1f;
                damage = (int)(damage * gPlayer.freshMultiplier);
            }
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
        public List<int> UsedProjs = new List<int>();
        private bool clearList = false;
        private int timer = 0;
        public float freshMultiplier = 1.0f;

        public override void ResetEffects()
        {
            if (timer > 0)
            {
                timer--;
            }
            else if (UsedProjs.Count > 0 && clearList)
            {
                clearList = false;
                UsedProjs.Clear();
                freshMultiplier = 1.0f;
            }
            float minMult = 0.2f;
            if (freshMultiplier < minMult)
            {
                freshMultiplier = minMult;
            }
        }

        /// <summary>
        /// To be called in ModProjectile.ModifyHitNPC
        /// </summary>
        /// <param name="type">Projectile type</param>
        /// <param name="modifiers"></param>
        public void ComboDamage(int type, ref NPC.HitModifiers modifiers)
        {
            timer = 180;
            if (UsedProjs.Contains(type))
            {
                modifiers.FinalDamage += UsedProjs.Count / 100 * 4;
                modifiers.FinalDamage *= freshMultiplier;
                clearList = true;
            }
            if (UsedProjs[0] == type)
            {

            }
        }
    }

    public class GauntletProjectile : GlobalProjectile
    {
        bool doGauntletStuff = false;

        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo itemSource)
            {
                if (itemSource.Item.type == ModContent.ItemType<AreusGauntlet>())
                {
                    doGauntletStuff = true;
                }
            }
            else if (Main.player[projectile.owner].HeldItem.type == ModContent.ItemType<AreusGauntlet>())
            {
                doGauntletStuff = true;
            }
            base.OnSpawn(projectile, source);
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (doGauntletStuff)
            {
                var player = Main.player[projectile.owner];
                var gPlayer = player.GetModPlayer<ElecGauntletPlayer>();

                if (gPlayer.UsedProjs.Contains(projectile.type))
                {
                    gPlayer.ComboDamage(projectile.type, ref modifiers);
                }
            }
        }
    }
}