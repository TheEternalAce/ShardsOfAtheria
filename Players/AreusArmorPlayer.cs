using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class AreusArmorPlayer : ModPlayer
    {
        public string[] chipNames = new[] { "", "", "" };

        public bool areusArmorPiece;
        public DamageClass classChip;
        public float areusDamage;

        public bool areusHead;
        public bool areusBody;
        public bool areusLegs;

        public bool guardSetPrevious;
        public bool guardSet;
        public bool soldierSet;
        public bool imperialSet;
        public bool royalSet;

        public int areusEnergy;
        public const int AREUS_ENERGY_MAX = 100;
        public const int EnergyTimerMax = 20;
        public int energyTimer;

        public bool WarriorSet => classChip == DamageClass.Melee;
        public bool RangerSet => classChip == DamageClass.Ranged;
        public bool MageSet => classChip == DamageClass.Magic;
        public bool CommanderSet => classChip == DamageClass.Summon;

        public bool ArmorSetCooldown => Player.ArmorSetCooldown();

        public override void SaveData(TagCompound tag)
        {
            tag.Add(nameof(chipNames), chipNames.ToList());
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey(nameof(chipNames)))
            {
                chipNames = tag.GetList<string>(nameof(chipNames)).ToArray();
            }
        }

        public override void ResetEffects()
        {
            SetBonusEffects();

            areusArmorPiece = false;
            classChip = DamageClass.Generic;
            areusHead = false;
            areusBody = false;
            areusLegs = false;
            guardSetPrevious = guardSet;
            guardSet = false;
            soldierSet = false;
            imperialSet = false;
            royalSet = false;
            areusDamage = 0f;
            if (areusEnergy > AREUS_ENERGY_MAX)
            {
                areusEnergy = AREUS_ENERGY_MAX;
            }
        }

        public void SetBonusEffects()
        {
            if (areusArmorPiece)
            {
                if (Main.playerInventory)
                {
                    ModContent.GetInstance<ChipsUISystem>().ShowChips();
                }

                if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[0] < 15 ||
                    Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[1] < 15)
                {
                    if (!ArmorSetCooldown)
                    {
                        int cooldownTime = 60;
                        if (guardSet && areusEnergy > 0)
                        {
                            if (WarriorSet)
                            {
                                GuardActive_Melee();
                            }
                            if (MageSet)
                            {
                                GuardActive_Magic();
                            }
                            if (RangerSet)
                            {
                                GuardActive_Ranged();
                            }
                            if (CommanderSet)
                            {
                                GuardActive_Summon();
                            }
                        }
                        if (soldierSet)
                        {
                            if (WarriorSet)
                            {
                                SoldierActive_Melee();
                                cooldownTime *= 10;
                            }
                            if (MageSet)
                            {
                                SoldierActive_Magic();
                            }
                            if (RangerSet)
                            {
                                SoldierActive_Ranged();
                                cooldownTime *= 15;
                            }
                            if (CommanderSet)
                            {
                                SoldierActive_Summon();
                            }
                        }
                        if (imperialSet)
                        {
                            if (WarriorSet)
                            {
                                ImperialActive_Melee();
                            }
                            if (MageSet)
                            {
                                ImperialActive_Magic();
                            }
                            if (RangerSet)
                            {
                                ImperialActive_Ranged();
                            }
                            if (CommanderSet)
                            {
                                ImperialActive_Summon();
                            }
                        }
                        if (royalSet)
                        {
                            if (WarriorSet)
                            {
                                RoyalActive_Melee();
                            }
                            if (MageSet)
                            {
                                RoyalActive_Magic();
                            }
                            if (RangerSet)
                            {
                                RoyalActive_Ranged();
                            }
                            if (CommanderSet)
                            {
                                RoyalActive_Summon();
                            }
                        }
                        Player.AddBuff<SetBonusCooldown>(cooldownTime);
                    }
                }
            }
        }

        public override void PostUpdate()
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<ElectricBarrier>()] > 0)
            {
                Player.statDefense += 20;
            }
        }

        private void GuardActive_Melee()
        {
            var projectiles = ShardsHelpers.ProjectileRing(Player.GetSource_FromThis(),
                Player.Center, 8, 1, 20f, ModContent.ProjectileType<AreusShockwave>(),
                30 + areusEnergy, 0f, Player.whoAmI);
            areusEnergy = 0;
            foreach (var projectile in projectiles)
            {
                projectile.DamageType = DamageClass.Melee;
                projectile.timeLeft = 15;
            }
            SoundEngine.PlaySound(SoundID.Item38, Player.Center);
        }
        private void GuardActive_Ranged()
        {
            if (!Player.HasBuff<ElectricMarksman>())
            {
                Player.AddBuff<ElectricMarksman>(18000);
            }
        }
        private void GuardActive_Magic()
        {
            if (areusEnergy >= AREUS_ENERGY_MAX)
            {
                areusEnergy = 0;
                SoundEngine.PlaySound(SoundID.NPCDeath56);
                float numberProjectiles = 3; // 3 shots
                float rotation = MathHelper.ToRadians(10);
                var source = Player.GetSource_FromThis();
                var position = Player.Center;
                var velocity = Main.MouseWorld - Player.Center;
                float speed = 14f;
                int type = ModContent.ProjectileType<ElectricWave>();
                int damage = 40;
                float knockback = 0;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    perturbedSpeed.Normalize();
                    perturbedSpeed *= speed;
                    Projectile.NewProjectile(source, position, perturbedSpeed,
                        type, damage, knockback, Player.whoAmI);
                }
            }
        }
        private void GuardActive_Summon()
        {
            if (areusEnergy >= AREUS_ENERGY_MAX)
            {
                if (!Player.HasBuff<ChargedMinions>())
                {
                    Player.AddBuff<ChargedMinions>(600);
                }
            }
        }

        private void SoldierActive_Melee()
        {
            var center = Player.Center;
            float speed = 0f;
            float radius = 75f;
            float rotation = MathHelper.ToRadians(360 / 2);
            for (int i = 0; i < 2; i++)
            {
                Vector2 position = center + new Vector2(1, 0).RotatedBy(rotation * i) * radius;
                Vector2 velocity = Vector2.Normalize(center - position) * speed;
                Projectile.NewProjectileDirect(Player.GetSource_FromThis(), position,
                    velocity, ModContent.ProjectileType<ElectricBarrier>(), 0, 0f,
                    Player.whoAmI, i);
            }
            SoundEngine.PlaySound(SoundID.NPCDeath45, Player.Center);
        }
        private void SoldierActive_Ranged()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath45, Player.Center);
            Player.AddBuff<ElectricVeil>(600);
        }
        private void SoldierActive_Magic()
        {

        }
        private void SoldierActive_Summon()
        {

        }

        private void ImperialActive_Melee()
        {

        }
        private void ImperialActive_Ranged()
        {

        }
        private void ImperialActive_Magic()
        {

        }
        private void ImperialActive_Summon()
        {

        }

        private void RoyalActive_Melee()
        {

        }
        private void RoyalActive_Ranged()
        {

        }
        private void RoyalActive_Magic()
        {

        }
        private void RoyalActive_Summon()
        {

        }

        public override void PostHurt(Player.HurtInfo info)
        {
            if (guardSet && classChip == DamageClass.Melee)
            {
                areusEnergy += 5;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (guardSet && classChip == DamageClass.Ranged)
            {
                areusEnergy++;
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Player.HasBuff<ElectricVeil>())
            {
                npc.AddBuff<ElectricShock>(300);
            }
        }
    }
}
