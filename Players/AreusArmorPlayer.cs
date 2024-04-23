using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar;
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

        public bool royalSet;

        public bool soldierSet;
        public bool bannerDamage;
        public bool bannerDefense;
        public bool bannerEndurance;
        public bool bannerMobility;
        public bool bannerAttackSpeed;
        public bool bannerResourceManagement;
        public bool bannerRegen;

        public bool WarriorSet => classChip.CountsAsClass(DamageClass.Melee);
        public bool RangerSet => classChip.CountsAsClass(DamageClass.Ranged);
        public bool MageSet => classChip.CountsAsClass(DamageClass.Magic);
        public bool CommanderSet => classChip.CountsAsClass(DamageClass.Summon);
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

                if ((Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[0] < 15) ||
                    (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[1] < 15))
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
                            areusEnergy = 0;
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
                            if (CommanderSet)
                            {
                                ImperialActive_Summon();
                            }
                            if (imperialVoid >= 33)
                            {
                                bool consumeVoid = false;
                                if (WarriorSet)
                                {
                                    ImperialActive_Melee();
                                    consumeVoid = true;
                                }
                                if (MageSet)
                                {
                                    ImperialActive_Magic();
                                    consumeVoid = true;
                                }
                                if (RangerSet)
                                {
                                    ImperialActive_Ranged();
                                    consumeVoid = true;
                                }
                                if (consumeVoid) ConsumeImperialVoid();
                            }
                        }
                        if (royalSet)
                        {
                            cooldownTime *= 10;
                            var npc = ShardsHelpers.FindClosestNPC(Main.MouseWorld, null, 100f);
                            if (npc != null)
                            {
                                Player.MinionAttackTargetNPC = npc.whoAmI;
                                npc.AddBuff<MarkedByAvatar>(3600);
                            }
                            if (CommanderSet)
                            {
                                cooldownTime *= 2;
                                RoyalActive_Summon();
                            }
                        }
                        if (cooldownTime > 600) cooldownTime = 600;
                        Player.AddBuff<SetBonusCooldown>(cooldownTime);
                    }
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (imperialSet)
            {
                ImperialVoidStar();
            }
            if (royalSet)
            {
                int type = ModContent.ProjectileType<TheRoyalCrown>();
                int headSlot = EquipLoader.GetEquipSlot(Mod, "RoyalCrown", EquipType.Head);
                if (Player.ownedProjectileCounts[type] == 0 && Player.head == headSlot)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, type, 0, 0);
                }
                type = ModContent.ProjectileType<EMAvatar>();
                if (Player.ownedProjectileCounts[type] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, type, 120, 0);
                }
            }
            if (Player.HasChipEquipped(ModContent.ItemType<FlightChip>()))
            {
                Player.wingTimeMax += 20;
            }
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

        private void RoyalActive_Summon()
        {
            Player.AddBuff<ChargingDrones>(121);
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
            if (guardSet && RangerSet)
            {
                areusEnergy++;
            }
            if (imperialSet)
            {
                ImperialOnHitEffect(target, hit);
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
