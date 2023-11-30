using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Projectiles.Summon.Minions;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public partial class AreusArmorPlayer : ModPlayer
    {
        public string[] chipNames = new[] { "", "", "" };

        public bool areusArmorPiece;
        public DamageClass classChip;
        public float areusDamage;

        public bool areusHead;
        public bool areusBody;
        public bool areusLegs;

        public bool imperialSet;

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
                            if (WarriorSet)
                            {
                                SoldierActive_Melee();
                            }
                            //if (MageSet)
                            //{
                            //    SoldierActive_Magic();
                            //}
                            if (RangerSet)
                            {
                                SoldierActive_Ranged();
                            }
                            //if (CommanderSet)
                            //{
                            //    SoldierActive_Summon();
                            //}
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

        public override void PostUpdate()
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<ElectricBarrier>()] > 0)
            {
                Player.statDefense += 20;
            }
            if (royalSet)
            {
                RoyalVoidStar();
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

        public override void PostHurt(Player.HurtInfo info)
        {
            if (guardSet && WarriorSet)
            {
                areusEnergy += 5;
            }
            if (soldierSet && CommanderSet)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<AreusDrone>()] < 6)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<AreusDrone>(),
                        (int)ClassDamage.ApplyTo(50), 0);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (guardSet && classChip == DamageClass.Ranged)
            {
                areusEnergy++;
            }
            RoyalOnHitEffect(hit);
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
