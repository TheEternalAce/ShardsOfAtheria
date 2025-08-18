using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor;
using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar;
using ShardsOfAtheria.Projectiles.Throwing;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public partial class AreusArmorPlayer : ModPlayer
    {
        public string[] chipNames = ["", "", ""];

        public DamageClass classChip = DamageClass.Default;
        public float areusDamage;

        public bool AreusArmorPiece => areusHead || areusBody || areusLegs;
        public bool areusHead;
        public bool areusBody;
        public bool areusLegs;
        public bool ninjaSet;

        public bool royalSet;

        public bool soldierSet;
        public bool bannerDamage;
        public bool bannerDefense;
        public bool bannerEndurance;
        public bool bannerMobility;
        public bool bannerAttackSpeed;
        public bool bannerResourceManagement;
        public bool bannerRegen;
        public bool bannerVelocity;

        public bool WarriorSetChip => classChip.CountsAsClass(DamageClass.Melee);
        public bool RangerSetChip => classChip.CountsAsClass(DamageClass.Ranged);
        public bool MageSetChip => classChip.CountsAsClass(DamageClass.Magic);
        public bool CommanderSetChip => classChip.CountsAsClass(DamageClass.Summon);
        public bool NinjaSetChip => classChip.CountsAsClass(DamageClass.Throwing);
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
                chipNames = [.. tag.GetList<string>(nameof(chipNames))];
            }
        }

        public override void ResetEffects()
        {
            SetBonusEffects();

            classChip = DamageClass.Default;
            areusHead = false;
            areusBody = false;
            areusLegs = false;
            ninjaSet = false;
            guardSetPrevious = guardSet;
            guardSet = false;
            soldierSet = false;
            imperialSet = false;
            royalSet = false;
            areusDamage = 0f;
            if (areusEnergy > AREUS_ENERGY_MAX)
                areusEnergy = AREUS_ENERGY_MAX;
            bannerDamage = false;
            bannerDefense = false;
            bannerEndurance = false;
            bannerMobility = false;
            bannerAttackSpeed = false;
            bannerResourceManagement = false;
            bannerRegen = false;
            bannerVelocity = false;
        }

        public void SetBonusEffects()
        {
            if (AreusArmorPiece)
            {
                if (Main.playerInventory) ModContent.GetInstance<ChipsUISystem>().ShowChips();

                if ((Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[0] < 15) ||
                    (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[1] < 15))
                {
                    if (!ArmorSetCooldown)
                    {
                        int cooldownTime = 60;
                        if (guardSet && areusEnergy > 0)
                        {
                            if (WarriorSetChip) GuardActive_Melee();
                            if (MageSetChip) GuardActive_Magic();
                            if (RangerSetChip) GuardActive_Ranged();
                            if (CommanderSetChip) GuardActive_Summon();
                            if (NinjaSetChip) GuardActive_Throwing();
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
                            if (CommanderSetChip) ImperialActive_Summon();
                            if (imperialVoid >= 33)
                            {
                                bool consumeVoid = false;
                                if (WarriorSetChip || MageSetChip || RangerSetChip || NinjaSetChip) consumeVoid = true;

                                if (WarriorSetChip) ImperialActive_Melee();
                                if (MageSetChip) ImperialActive_Magic();
                                if (RangerSetChip) ImperialActive_Ranged();
                                if (NinjaSetChip) ImperialActive_Thrown();

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
                            if (CommanderSetChip)
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
            if (imperialSet) ImperialVoidStar();
            if (royalSet)
            {
                int type = ModContent.ProjectileType<TheRoyalCrown>();
                int headSlot = EquipLoader.GetEquipSlot(Mod, "RoyalCrown", EquipType.Head);
                if (Player.ownedProjectileCounts[type] == 0 && Player.head == headSlot)
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, type, 0, 0);
                type = ModContent.ProjectileType<EMAvatar>();
                if (Player.ownedProjectileCounts[type] == 0)
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, type, 120, 0);
            }
            if (Player.HasChipEquipped(ModContent.ItemType<FlightChip>())) Player.wingTimeMax += 20;
            if (bannerDamage) Player.GetDamage(DamageClass.Generic) += 0.15f;
            if (bannerDefense) Player.statDefense += 15;
            if (bannerEndurance) Player.endurance += 0.08f;
            if (bannerMobility) Player.moveSpeed += 0.15f;
            if (bannerAttackSpeed) Player.GetDamage(DamageClass.Generic) += 0.15f;
            if (bannerResourceManagement) Player.manaCost -= 0.15f;
            if (bannerRegen) Player.manaRegen += 10;
        }

        public override void UpdateLifeRegen()
        {
            if (bannerRegen) Player.lifeRegen += 12;
        }

        private void RoyalActive_Summon()
        {
            Player.AddBuff<ChargingDrones>(121);
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (bannerResourceManagement && Main.rand.NextBool(3)) return false;
            return base.CanConsumeAmmo(weapon, ammo);
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            if (guardSet && WarriorSetChip)
            {
                int energyToAdd = info.Damage / 3;
                if (energyToAdd < 5) energyToAdd = 5;
                areusEnergy += energyToAdd;
            }
            if (soldierSet)
            {
                if (RangerSetChip && Player.HasBuff<ElectricVeil>())
                    ShardsHelpers.ProjectileRing(Player.GetSource_FromThis(), Player.Center, 8, 1f, 15f, ProjectileID.ThunderSpearShot, 50, 6f);
            }
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (ninjaSet && item.DamageType.CountsAsClass(DamageClass.Throwing)) velocity *= 1.2f;
            if (bannerVelocity) velocity *= 1.2f;
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (NinjaSetChip && imperialSet && Player.HasBuff<ShadeState>() && item.DamageType.CountsAsClass(DamageClass.Throwing))
            {
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(15);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    perturbedSpeed.Normalize();
                    perturbedSpeed *= 8f;
                    Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<VoidOrb>(), (int)ClassDamage.ApplyTo(120 + imperialVoid), knockback);
                }
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (guardSet)
            {
                if (RangerSetChip) areusEnergy++;
                if (NinjaSetChip && areusEnergy >= 20 && thrownEnergyBurstCooldown == 0 && hit.DamageType.CountsAsClass(DamageClass.Throwing))
                {
                    areusEnergy -= 20;
                    int amount = 5;
                    float rotation = MathHelper.ToRadians(360 / amount);
                    for (int i = 0; i < amount; i++)
                    {
                        float speed = 6f * Main.rand.NextFloat(0.33f, 1f);
                        Vector2 position = target.Center - Vector2.UnitY.RotatedBy(rotation * i);
                        Vector2 velocity = Vector2.Normalize(target.Center - position) * speed;
                        Projectile.NewProjectile(target.GetSource_OnHurt(Player), position, velocity, ModContent.ProjectileType<ElectricSpark>(),
                            22, 0, Player.whoAmI);
                    }
                    SoundEngine.PlaySound(SoundID.NPCHit53, target.Center);
                    thrownEnergyBurstCooldown = 30;
                }
            }
            if (imperialSet) ImperialOnHitEffect(target, hit);
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Player.HasBuff<ElectricVeil>()) npc.AddBuff<ElectricShock>(300);
        }
    }
}
