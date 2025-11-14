using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.PlayerDebuff.SinDebuffs;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class SinnerPlayer : ModPlayer
    {
        public const int ENVY = 1;
        public const int GLUTTONY = 2;
        public const int GREED = 3;
        public const int LUST = 4;
        public const int PRIDE = 5;
        public const int SLOTH = 6;
        public const int WRATH = 7;
        public int sinID = -1;

        // Envy
        public NPC target;
        public int targetStrikes;

        // Gluttony
        public int hunger = 100;
        public const int HUNGER_MAX = 100;
        public int hungerTimer = 60;
        public int foodCoolDown = 0;
        public const int FOOD_COOLDOWN_MAX = 240;

        // Pride
        public int noHitTimer = 0;
        public float flawlessBuff = 0;
        public int attacksMade = 0;
        public int attacksHit = 0;
        public int attackTimer = 0;
        public int hitCooldown = 0;
        public int hitDelay = 0;
        public bool prideful = false;

        // Sloth
        bool motiveSet = false;

        // Wrath
        int lastDamageTaken;
        public int retainFuryTime;

        public bool SinActive => sinID >= 0;

        #region Envy
        void EnvyKill(NPC target)
        {
            if (sinID != ENVY) return;
            if (target.life <= 0 && target == this.target)
            {
                target = null;
                targetStrikes = 0;
            }
        }
        void EnvyModifyHit(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (sinID != ENVY) return;
            if (this.target == null)
            {
                this.target = target;
                targetStrikes = 0;
            }
            else if (target != this.target) modifiers.FinalDamage -= 0.4f;
            else
            {
                modifiers.FlatBonusDamage += targetStrikes * 3;
                targetStrikes++;
            }
        }
        #endregion

        #region Gluttony
        void GluttonyPreUpdate()
        {
            if (sinID != GLUTTONY) return;
            if (hunger > 0 && --hungerTimer <= 0)
            {
                hunger--;
                if (!(Player.HasBuff(BuffID.WellFed) || Player.HasBuff(BuffID.WellFed2) || Player.HasBuff(BuffID.WellFed3))) hunger--;
                hungerTimer = 30;
            }
            if (hunger > 600) Player.AddBuff<OverStuffed>(121);
            else if (hunger < 0) hunger = 0;
        }
        void GluttonyModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (sinID != GLUTTONY) return;
            if (hunger > 1)
            {
                int maxReduction = ShardsHelpers.ScaleByProggression(Player, 10);
                int reduction = Math.Min(hunger / 2, maxReduction);
                modifiers.FinalDamage.Flat -= maxReduction;
                hunger -= reduction * 2;
            }
        }
        void GluttonyPickUp(Item item)
        {
            if (sinID != GLUTTONY) return;
            if (item.type == ItemID.Heart || item.type == ItemID.CandyApple || item.type == ItemID.CandyCane ||
                item.type == ItemID.ManaCloakStar || item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum)
            {
                Player.AddBuff<GluttonyAcid>(150);
            }
        }
        void GluttonyHit(NPC.HitInfo hit)
        {
            if (sinID != GLUTTONY) return;
            if (foodCoolDown <= 0)
            {
                float foodChance = 0.1f;
                if (hit.Crit || target.life <= 0) foodChance = 1f;
                if (target.lifeMax > 5 && Main.rand.NextFloat() < foodChance)
                {
                    float ai0 = 0;
                    if (target.life <= 0) ai0 = 1;
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, ai0);
                    foodCoolDown = FOOD_COOLDOWN_MAX;
                }
            }
        }
        #endregion

        #region Greed
        void GreedPickUp(Item item)
        {
            if (sinID != GREED) return;
            if (item.value > 10000) Player.AddBuff<GreedFire>(150);
        }
        void GreedFire()
        {
            if (Player.HasBuff<GreedFire>())
            {
                int flames = Player.statLifeMax2 / 20;
                if (flames % 2 != 0) flames++;
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= flames;
            }
        }
        void GreedDeath()
        {
            if (sinID != GREED) return;
            int i = 0;
            foreach (var item in Player.inventory)
            {
                if (item.IsAir) continue;
                if (i <= 9)
                {
                    i++;
                    continue;
                }
                if (Main.rand.NextBool()) continue;
                Item.NewItem(Player.GetSource_Death(), Player.Hitbox, item);
                item.TurnToAir();
                i++;
            }
        }
        #endregion

        #region Pride
        void PridePreUpdate()
        {
            if (sinID != PRIDE)
            {
                flawlessBuff = 0;
                noHitTimer = 0;
                attacksMade = 0;
                attacksHit = 0;
                hitCooldown = 0;
                prideful = false;
            }
            else
            {
                if (!Player.InCombat() && flawlessBuff > 0)
                {
                    noHitTimer--;
                    if (noHitTimer <= -60)
                    {
                        flawlessBuff -= .02f;
                        noHitTimer = 0;
                    }
                }

                if (attacksMade < attacksHit) attacksHit = attacksMade;

                if (attackTimer > 0)
                {
                    if (--attackTimer == 0)
                    {
                        if (attacksHit > attacksMade * 0.8) prideful = true;
                        else if (attacksHit < attacksMade * 0.75)
                        {
                            Player.AddBuff<Embarassment>(300);
                            attacksMade = 0;
                            attacksHit = 0;
                            prideful = false;
                        }
                    }
                }

                if (hitCooldown > 0) hitCooldown--;
            }
        }
        void PridePostUpdate()
        {
            if (sinID != PRIDE) return;
            if (Player.InCombat()) noHitTimer++;
            if (noHitTimer >= 600)
            {
                flawlessBuff += .02f;
                noHitTimer = 0;
            }
            Player.GetDamage(DamageClass.Generic) += flawlessBuff;
            if (prideful) Player.moveSpeed += .15f;
        }
        void PrideAddSuccessfulAttack(NPC target, bool otherCondition)
        {
            if (sinID == PRIDE && target.lifeMax > 5 && Player.InCombat() && attacksHit < attacksMade && hitCooldown == 0 && otherCondition)
            {
                hitCooldown = Player.itemTime;
                attacksHit++;
            }
        }
        public void PrideCancelAttack()
        {
            if (sinID == PRIDE && Player.InCombat())
                attacksMade--;
        }
        void PrideHurt()
        {
            flawlessBuff = 0;
            noHitTimer = 0;
        }
        void PrideDeath()
        {
            prideful = false;
            flawlessBuff = 0;
            noHitTimer = 0;
            attacksMade = 0;
            attacksHit = 0;
            hitCooldown = 0;
        }
        #endregion

        #region Lust
        void LustItemHit(NPC target, Item item)
        {
            if (sinID != LUST) return;
            if (Main.rand.NextBool(50)) Item.NewItem(target.GetSource_OnHurt(item), target.getRect(), ItemID.Heart);
        }
        void LustProjectileHit(NPC target, Projectile projectile)
        {
            if (sinID != LUST) return;
            int heartChance = 100;
            if (projectile.DamageType.CountsAsClass(DamageClass.Melee)) heartChance = 50;
            if (Main.rand.NextBool(heartChance)) Item.NewItem(target.GetSource_OnHurt(projectile), target.getRect(), ItemID.Heart);
        }
        void LustCrit(ref Player.HurtModifiers modifiers)
        {
            if (sinID != LUST) return;
            float critChance = 0.25f;
            if (Main.expertMode) critChance += 0.06f;
            if (Main.masterMode) critChance += 0.12f;
            if (Main.hardMode) critChance += 0.08f;
            if (NPC.downedPlantBoss) critChance += 0.06f;
            if (NPC.downedAncientCultist) critChance += 0.12f;
            if (Main.rand.NextFloat() < critChance)
            {
                modifiers.FinalDamage *= 2;
                SoundEngine.PlaySound(SoundID.AbigailCry, Player.Center);
            }
        }
        #endregion

        #region Sloth
        void SlothPreUpdate()
        {
            if (sinID != SLOTH) return;
            if (motiveSet && Main.dayTime && Main.time == 0)
                motiveSet = false;
            if (!motiveSet && Main.dayTime && Main.time == 1)
            {
                Player.TryClearBuff<Apathy>();
                Player.TryClearBuff<Motivation>();
                int motive = ModContent.BuffType<Apathy>();
                if (Main.rand.NextBool(3)) motive = ModContent.BuffType<Motivation>();
                Player.AddBuff(motive, 2);
                motiveSet = true;
            }
        }
        void SlothPostUpdate()
        {
            if (sinID != SLOTH) return;
            Player.lifeRegen += 16;
            if (Player.mount.Active) Player.statDefense += 5;
        }
        #endregion

        #region Wrath
        public void WrathHurt(Player.HurtInfo info)
        {
            if (sinID != WRATH) return;
            lastDamageTaken = info.Damage;
        }
        public void WrathCrit(ref Player.HurtModifiers modifiers)
        {
            if (sinID != WRATH) return;
            if (Main.rand.NextFloat() < 0.04f)
            {
                Player.AddBuff<Fury>(600);
                modifiers.FinalDamage *= 2f;
                SoundEngine.PlaySound(SoundID.ScaryScream, Player.Center);
            }
        }
        public void WrathModifyHit(ref NPC.HitModifiers modifiers)
        {
            if (sinID != WRATH) return;
            modifiers.FlatBonusDamage += lastDamageTaken;
            lastDamageTaken = 0;
            if (Player.HasBuff<Fury>() && target.CanBeChasedBy()) retainFuryTime = 60;
        }
        #endregion

        public override void SaveData(TagCompound tag)
        {
            tag["selectedSin"] = sinID;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.TryGet("selectedSin", out int sin)) sinID = sin;
        }

        public override void ResetEffects()
        {
            if (foodCoolDown < 0) foodCoolDown = 0;
            else if (foodCoolDown > 0) foodCoolDown--;

            if (retainFuryTime > 0) retainFuryTime--;
        }

        public override void PreUpdate()
        {
            GluttonyPreUpdate();
            PridePreUpdate();
            SlothPreUpdate();
        }

        public override void PostUpdate()
        {
            PridePostUpdate();
            SlothPostUpdate();
        }

        public override void OnRespawn()
        {
            hunger = 3600;
        }

        public override void OnEnterWorld()
        {
            hunger = 1800;
            hungerTimer = 120;
            sinID = -1;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff<OverStuffed>()) Player.lifeRegen /= 3;
            GreedFire();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            EnvyModifyHit(target, ref modifiers);
            WrathModifyHit(ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            GluttonyHit(hit);
            EnvyKill(target);
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            PrideAddSuccessfulAttack(target, !item.IsTool());
            LustItemHit(target, item);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            PrideAddSuccessfulAttack(target, !proj.NonWhipSummon());
            LustProjectileHit(target, proj);
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            GluttonyModifyHurt(ref modifiers);
            LustCrit(ref modifiers);
            WrathCrit(ref modifiers);
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            PrideHurt();
            WrathHurt(info);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            GreedDeath();
        }

        public override void UpdateDead()
        {
            PrideDeath();
        }

        public override bool OnPickup(Item item)
        {
            GluttonyPickUp(item);
            GreedPickUp(item);
            return base.OnPickup(item);
        }
    }
}
