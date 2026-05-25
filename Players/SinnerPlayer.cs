using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Sinner;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Players
{
    public partial class CardinalSoulPlayer
    {
        public bool EnviousSinner => cardinalSoul == CardinalSoulID.Envy;
        public bool GluttonousSinner => cardinalSoul == CardinalSoulID.Gluttony;
        public bool GreedySinner => cardinalSoul == CardinalSoulID.Greed;
        public bool LustfulSinner => cardinalSoul == CardinalSoulID.Lust;
        public bool PridefulSinner => cardinalSoul == CardinalSoulID.Pride;
        public bool SlothfulSinner => cardinalSoul == CardinalSoulID.Sloth;
        public bool WrathfulSinner => cardinalSoul == CardinalSoulID.Wrath;
        public bool Sinner => cardinalSoul > 0 && cardinalSoul < 8;

        public bool SoulActive => cardinalSoul > 0;

        public int cardinalSoul;
        private int storedDamage;

        #region Envy
        public int EnvyQuarrel
        {
            get => envyQuarrel;
            set
            {
                envyQuarrel = value;
                envyQuarrelTimer = 180;
            }
        }
        int envyQuarrel;
        int envyQuarrelTimer;
        public int envyQuarrelTarget = -1;
        public int envyTargetID;
        public bool envyTargetMarked;
        void EnvyPostUpdate()
        {
            if (envyQuarrel >= 0)
            {
                if (envyQuarrelTimer >= 0) envyQuarrelTimer--;
                else envyQuarrel--;
            }
            if (!EnviousSinner) return;
            Player.blind = true;
        }
        void EnvyModifyHit(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!EnviousSinner) return;
            if (envyTargetID > -1 && target.whoAmI != envyTargetID) modifiers.FinalDamage -= 0.5f;
            else modifiers.FlatBonusDamage += Player.statDefense / 10 + 3;
            envyTargetID = target.whoAmI;
        }
        void EnvyHit(NPC target)
        {
            if (!EnviousSinner) return;
            if (target.life <= 0 && target.whoAmI == envyTargetID)
                envyTargetID = -1;
        }
        #endregion

        #region Gluttony
        public int gluttonyHunger = 100;
        public const int GluttonyHungerMax = 3600;
        public int gluttonyHungerTimer = 60;
        public int gluttonyFoodCoolDown = 0;
        public const int GluttonyFoodCooldownMax = 600;
        public const int GluttonyAcidDuration = 300;
        void GluttonySetHunger()
        {
            gluttonyHunger = 3600;
            gluttonyHungerTimer = 120;
        }
        void GluttonyPreUpdate()
        {
            if (!GluttonousSinner) return;
            if (gluttonyHunger > 0)
            {
                if (--gluttonyHungerTimer <= 0)
                {
                    int hungerDecay = 4;
                    int hungerDecayTimer = 30;
                    if (Player.HasBuff(BuffID.WellFed3)) hungerDecay -= 3;
                    else if (Player.HasBuff(BuffID.WellFed2)) hungerDecay -= 2;
                    else if (Player.HasBuff(BuffID.WellFed)) hungerDecay--;
                    if (Player.HasBuff<GluttonyAcid>())
                    {
                        hungerDecay += 6;
                        hungerDecayTimer -= 15;
                    }
                    if (Player.sleeping.FullyFallenAsleep)
                    {
                        hungerDecay = 1;
                        hungerDecayTimer += 90;
                    }
                    if (gluttonyHunger > GluttonyHungerMax * 3 / 4 && (Player.controlLeft || Player.controlRight)) hungerDecay += 4;
                    gluttonyHunger -= hungerDecay;
                    gluttonyHungerTimer = hungerDecayTimer;
                }
            }
            if (gluttonyHunger > GluttonyHungerMax) Player.AddBuff<OverStuffed>(121);
            else if (gluttonyHunger < 0) gluttonyHunger = 0;
            else if (gluttonyHunger == 0) Player.AddBuff(BuffID.Starving, 180);
            if (Player.ItemAnimationActive && Player.HeldItem.IsWeapon())
            {
                float range = 200f;
                ShardsHelpers.DustAura(Player.Center, range, DustID.Blood, 20, Player.velocity);
            }
        }
        void GluttonyRegenDebuff()
        {
            if (Player.HasBuff<GluttonyAcid>()) RegenDebuff();
            if (Player.HasBuff<OverStuffed>()) Player.lifeRegen /= 3;
        }
        void GluttonyModifyHurt(ref Player.HurtModifiers modifiers)
        {
            modifiers.ModifyHurtInfo += StoreDamage;
            if (!GluttonousSinner) return;
            if (gluttonyHunger > 3 && storedDamage > Player.statLifeMax * 0.05f)
            {
                const float multiplier = 0.0005f;
                float minReduction = ShardsHelpers.ProggressionValue(Player, [0.95f, -0.05f, -0.05f, -0.05f, -0.05f]);
                float maxReduction = 1f - gluttonyHunger * multiplier;
                float reduction = Math.Max(maxReduction, minReduction);
                int consumption = (int)((reduction - 1f) / multiplier);
                modifiers.IncomingDamageMultiplier *= reduction;
                gluttonyHunger += consumption;
            }
        }
        private void StoreDamage(ref Player.HurtInfo info)
        {
            storedDamage = info.Damage;
        }
        void GluttonyPickUp(Item item)
        {
            if (!GluttonousSinner) return;
            if (item.type == ItemID.Heart || item.type == ItemID.CandyApple || item.type == ItemID.CandyCane ||
                item.type == ItemID.ManaCloakStar || item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum)
            {
                Player.AddBuff<GluttonyAcid>(GluttonyAcidDuration);
            }
        }
        private static void GluttonyHeal(On_Player.orig_Heal orig, Player self, int amount)
        {
            orig(self, amount);
            if (!self.CardinalSoul().GluttonousSinner) return;
            if (amount > self.statLifeMax2 * 0.1f)
                self.AddBuff<GluttonyAcid>(GluttonyAcidDuration);
        }
        private void On_GluttonyAddBuff(On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
        {
            orig(self, type, timeToAdd, quiet, foodHack);
            if (Main.debuff[type]) self.AddBuff<GluttonyAcid>(GluttonyAcidDuration);
        }
        void GluttonyHit(NPC target, NPC.HitInfo hit)
        {
            if (!GluttonousSinner) return;
            float range = 200f;
            bool canBeChased = target.chaseable && target.lifeMax > 5 && !target.immortal;
            if (canBeChased && Vector2.Distance(Player.Center, target.Center) < range && Player.ItemAnimationActive)
            {
                float foodChance = 0.33f;
                int hungerRefill = 120;
                if (target.life <= 0 || target.boss) foodChance = 1f;
                else
                {
                    if (hit.DamageType.CountsAsClass(DamageClass.Melee))
                        foodChance += 47f;
                    if (hit.Crit) foodChance += 0.17f;
                }
                if (gluttonyHunger >= GluttonyHungerMax) foodChance *= 0.4f;
                if (hit.DamageType.CountsAsClass(DamageClass.Summon)) foodChance *= 0.5f;
                if (Main.rand.NextFloat() < foodChance && gluttonyFoodCoolDown <= 0)
                {
                    if (hit.DamageType.CountsAsClass(DamageClass.Melee))
                        hungerRefill += 120;
                    if (target.life <= 0) hungerRefill += 180;
                    else if (gluttonyHunger > GluttonyHungerMax * 3 / 4) gluttonyFoodCoolDown = GluttonyFoodCooldownMax;
                    if (gluttonyHunger < GluttonyHungerMax * 2)
                        gluttonyHunger += hungerRefill;
                    else gluttonyHunger = GluttonyHungerMax * 2;
                    CombatText.NewText(Player.Hitbox, Color.Yellow, hungerRefill);
                }
            }
        }
        #endregion

        #region Greed
        void GreedPickUp(Item item)
        {
            if (!GreedySinner) return;
            if (item.value > 10000) Player.AddBuff<GreedFire>(150);
        }
        void GreedFire()
        {
            if (Player.HasBuff<GreedFire>()) RegenDebuff();
        }
        void GreedDeath()
        {
            if (!GreedySinner) return;
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

        #region Lust
        void LustPostUpdate()
        {
            if (!LustfulSinner) return;
            Player.GetDamage(DamageClass.Generic) -= 0.2f;
            Player.moveSpeed -= 0.1f;
        }
        void LustItemHit(NPC target, Item item)
        {
            if (!LustfulSinner) return;
            if (Main.rand.NextBool(50)) Item.NewItem(target.GetSource_OnHurt(item), target.getRect(), ItemID.Heart);
        }
        void LustProjectileHit(NPC target, Projectile projectile)
        {
            if (!LustfulSinner) return;
            int heartChance = 100;
            if (projectile.DamageType.CountsAsClass(DamageClass.Melee)) heartChance = 50;
            if (Main.rand.NextBool(heartChance)) Item.NewItem(target.GetSource_OnHurt(projectile), target.getRect(), ItemID.Heart);
        }
        void LustCrit(ref Player.HurtModifiers modifiers)
        {
            if (!LustfulSinner) return;
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

        #region Pride
        public int prideNoHitTimer = 0;
        public float prideSuaveBuff = 0;
        public int prideAttacksMade = 0;
        public int prideAttacksHit = 0;
        public int prideAttackTimer = 0;
        public int prideHitCooldown = 0;
        public int prideHitDelay = 0;
        public bool prideEgo = false;
        void PridePreUpdate()
        {
            if (!PridefulSinner)
            {
                prideSuaveBuff = 0;
                prideNoHitTimer = 0;
                prideAttacksMade = 0;
                prideAttacksHit = 0;
                prideHitCooldown = 0;
                prideEgo = false;
            }
            else
            {
                if (!Player.InCombat() && prideSuaveBuff > 0)
                {
                    prideNoHitTimer--;
                    if (prideNoHitTimer <= -60)
                    {
                        prideSuaveBuff -= .02f;
                        prideNoHitTimer = 0;
                    }
                }

                if (prideAttacksMade < prideAttacksHit) prideAttacksHit = prideAttacksMade;

                if (prideAttackTimer > 0)
                {
                    if (--prideAttackTimer == 0)
                    {
                        if (prideAttacksHit > prideAttacksMade * 0.8) prideEgo = true;
                        else if (prideAttacksHit < prideAttacksMade * 0.75)
                        {
                            Player.AddBuff<Embarassment>(300);
                            prideAttacksMade = 0;
                            prideAttacksHit = 0;
                            prideEgo = false;
                        }
                    }
                }

                if (prideHitCooldown > 0) prideHitCooldown--;
            }
        }
        void PridePostUpdate()
        {
            if (!PridefulSinner) return;
            if (Player.InCombat()) prideNoHitTimer++;
            if (prideNoHitTimer >= 600)
            {
                prideSuaveBuff += .02f;
                prideNoHitTimer = 0;
            }
            Player.GetDamage(DamageClass.Generic) += prideSuaveBuff;
            if (prideEgo) Player.moveSpeed += .15f;
        }
        void PrideAddSuccessfulAttack(NPC target, bool otherCondition)
        {
            if (PridefulSinner && target.lifeMax > 5 && Player.InCombat() && prideAttacksHit < prideAttacksMade && prideHitCooldown == 0 && otherCondition)
            {
                prideHitCooldown = Player.itemTime;
                prideAttacksHit++;
            }
        }
        public void PrideCancelAttack()
        {
            if (PridefulSinner && Player.InCombat())
                prideAttacksMade--;
        }
        void PrideRegen()
        {
            if (Player.HasBuff<EgoBoost>()) Player.lifeRegen += 10;
        }
        void PrideCoin()
        {
            if (!PridefulSinner) return;
            int coin = ModContent.ProjectileType<PrideGold>();
            if (Player.IsLocal() && Player.BuyItem(Item.buyPrice(0, 1)) && Player.ownedProjectileCounts[coin] < 4)
            {
                Player.ChangeDir(Main.MouseWorld.X > Player.Center.X ? 1 : -1);
                SoundEngine.PlaySound(SoA.Coin, Player.Center);
                Vector2 velocity = (Vector2.Normalize(Main.MouseWorld - Player.Center) * 10f + Player.velocity).RotatedBy(MathHelper.ToRadians(-15) * Player.direction);
                int damage = ShardsHelpers.ProggressionValue(Player, [10, 10, 20, 20, 30]);
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, velocity, coin, damage, 0.5f);
                if (Player.InCombat())
                {
                    prideAttacksMade++;
                    if (prideAttackTimer <= 0) prideAttackTimer = 300;
                }
            }
        }
        void PrideHurt()
        {
            prideSuaveBuff = 0;
            prideNoHitTimer = 0;
        }
        void PrideDeath()
        {
            prideEgo = false;
            prideSuaveBuff = 0;
            prideNoHitTimer = 0;
            prideAttacksMade = 0;
            prideAttacksHit = 0;
            prideHitCooldown = 0;
        }
        #endregion

        #region Sloth
        bool slothMotiveSet = false;
        void SlothPreUpdate()
        {
            if (!SlothfulSinner) return;
            if (slothMotiveSet && Main.dayTime && Main.time == 0)
                slothMotiveSet = false;
            if (!slothMotiveSet && Main.dayTime && Main.time == 1)
            {
                Player.TryClearBuff<Apathy>();
                Player.TryClearBuff<Motivation>();
                int motive = ModContent.BuffType<Apathy>();
                if (Main.rand.NextBool(3)) motive = ModContent.BuffType<Motivation>();
                Player.AddBuff(motive, 2);
                slothMotiveSet = true;
            }
            Player.aggro += 400;
        }
        void SlothPostUpdate()
        {
            if (!SlothfulSinner) return;
            Player.lifeRegen += 16;
            if (Player.mount.Active) Player.statDefense += 5;
        }
        #endregion

        #region Wrath
        int wrathLastDamageTaken;
        public int wrathRetainFuryTime;
        public void WrathRage()
        {
            if (!WrathfulSinner) return;
        }
        public void WrathPostUpdate()
        {
            if (!WrathfulSinner) return;
            Player.endurance -= 0.1f;
        }
        public void WrathCrit(ref Player.HurtModifiers modifiers)
        {
            if (!WrathfulSinner) return;
            modifiers.ModifyHurtInfo += StoreDamage;
            if (Main.rand.NextFloat() < 0.04f && storedDamage > 10)
            {
                Player.AddBuff<Fury>(600);
                modifiers.FinalDamage *= 2f;
                SoundEngine.PlaySound(SoundID.ScaryScream, Player.Center);
            }
        }
        public void WrathHurt(Player.HurtInfo info)
        {
            if (!WrathfulSinner) return;
            wrathLastDamageTaken = info.Damage;
        }
        public void WrathModifyHit(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!WrathfulSinner) return;
            modifiers.FlatBonusDamage += wrathLastDamageTaken;
            wrathLastDamageTaken = 0;
            if (Player.HasBuff<Fury>() && target.CanBeChasedBy()) wrathRetainFuryTime = 60;
        }
        #endregion

        private void RegenDebuff()
        {
            int rate = Player.statLifeMax2 / 20;
            if (rate % 2 != 0) rate++;
            if (Player.lifeRegen > 0)
                Player.lifeRegen = 0;
            Player.lifeRegenTime = 0;
            Player.lifeRegen -= rate;
        }
    }
}
