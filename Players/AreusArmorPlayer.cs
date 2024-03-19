using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public partial class AreusArmorPlayer : ModPlayer
    {
        public string[] chipNames = ["", "", ""];

        public bool areusArmorPiece;
        public DamageClass classChip;
        public float areusDamage;

        public bool areusHead;
        public bool areusBody;
        public bool areusLegs;

        public bool imperialSet;

        public bool soldierSet;
        public bool bannerDamage;
        public bool bannerDefense;
        public bool bannerEndurance;
        public bool bannerMobility;
        public bool bannerAttackSpeed;
        public bool bannerResourceManagement;
        public bool bannerRegen;

        public bool WarriorSet => classChip == DamageClass.Melee;
        public bool RangerSet => classChip == DamageClass.Ranged;
        public bool MageSet => classChip == DamageClass.Magic;
        public bool CommanderSet => classChip == DamageClass.Summon;

        public StatModifier ClassDamage => Player.GetTotalDamage(classChip);

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
            bannerDamage = false;
            bannerDefense = false;
            bannerEndurance = false;
            bannerMobility = false;
            bannerAttackSpeed = false;
            bannerResourceManagement = false;
            bannerRegen = false;
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
                            cooldownTime *= 10;
                            var velocity = Main.MouseWorld - Player.Center;
                            velocity.Normalize();
                            var banner = Projectile.NewProjectileDirect(Player.GetSource_FromThis(), Player.Center, velocity * 16f,
                                ModContent.ProjectileType<AreusHoloBannerProjector>(), 0, 0);
                            banner.DamageType = classChip;
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
                            if (CommanderSet)
                            {
                                RoyalActive_Summon();
                            }
                            else if (royalVoid >= 33)
                            {
                                cooldownTime = 0;
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
                                ConsumeRoyalVoid();
                            }
                        }
                        Player.AddBuff<SetBonusCooldown>(cooldownTime);
                    }
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (royalSet)
            {
                RoyalVoidStar();
            }
            if (Player.HasChipEquipped(ModContent.ItemType<FlightChip>()))
            {
                Player.wingTimeMax += 20;
            }
            //if (imperialSet)
            //{
            //    if (CommanderSet)
            //    {
            //        if (Player.ownedProjectileCounts[ModContent.ProjectileType<AreusDrone>()] < 3)
            //        {
            //            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<AreusDrone>(),
            //                (int)ClassDamage.ApplyTo(80), 0);
            //        }
            //    }
            //}
            if (bannerDamage) Player.GetDamage(DamageClass.Generic) += 0.15f;
            if (bannerDefense) Player.statDefense += 15;
            if (bannerEndurance) Player.endurance += 0.08f;
            if (bannerMobility) Player.moveSpeed += 0.15f;
            if (bannerAttackSpeed) Player.GetDamage(DamageClass.Generic) += 0.15f;
            if (bannerResourceManagement) Player.manaCost -= 0.15f;
            if (bannerRegen) Player.manaRegen += Player.statManaMax2 / 3;
        }

        public override void UpdateLifeRegen()
        {
            if (bannerRegen)
            {
                Player.lifeRegen += 12;
            }
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

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (bannerResourceManagement)
            {
                return Main.rand.NextFloat() < 0.33f;
            }
            return base.CanConsumeAmmo(weapon, ammo);
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            if (guardSet && WarriorSet)
            {
                int energyToAdd = info.Damage / 3;
                if (energyToAdd < 5)
                {
                    energyToAdd = 5;
                }
                areusEnergy += energyToAdd;
            }
            if (soldierSet)
            {
                if (RangerSet && Player.HasBuff<ElectricVeil>())
                {
                    ShardsHelpers.ProjectileRing(Player.GetSource_FromThis(), Player.Center, 8, 1f, 15f, ProjectileID.ThunderSpearShot, 50, 6f);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (guardSet && classChip == DamageClass.Ranged)
            {
                areusEnergy++;
            }
            if (royalSet)
            {
                RoyalOnHitEffect(target, hit);
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
