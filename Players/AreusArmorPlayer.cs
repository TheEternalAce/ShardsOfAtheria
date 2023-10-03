using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Items.Armor.Areus.Guard;
using ShardsOfAtheria.Items.Armor.Areus.Imperial;
using ShardsOfAtheria.Items.Armor.Areus.Royal;
using ShardsOfAtheria.Items.Armor.Areus.Soldier;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class AreusArmorPlayer : ModPlayer
    {
        public string[] chipNames = ShardsHelpers.SetEmptyStringArray(3);

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

        public int abilityTimer;
        public const int AbilityTimerMax = 60;

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
            if (abilityTimer > 0)
            {
                abilityTimer--;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (areusArmorPiece)
            {
                if (Main.playerInventory)
                {
                    ModContent.GetInstance<ChipsUISystem>().ShowChips();
                }

                if (SoA.ArmorSetBonusActive.JustPressed)
                {
                    if (abilityTimer == 0)
                    {
                        if (guardSet && areusEnergy > 0)
                        {
                            if (classChip == DamageClass.Melee)
                            {
                                GuardArmor_Melee();
                            }
                            if (classChip == DamageClass.Magic)
                            {
                                GuardArmor_Magic();
                            }
                            if (classChip == DamageClass.Ranged)
                            {
                                GuardArmor_Ranged();
                            }
                            if (classChip == DamageClass.Summon)
                            {
                                GuardArmor_Summon();
                            }
                        }
                        abilityTimer = AbilityTimerMax;
                    }
                }
            }
        }

        private void GuardArmor_Melee()
        {
            var projectiles = ShardsHelpers.ProjectileRing(Player.GetSource_FromThis(),
                Player.Center, 8, 1, 20f, ModContent.ProjectileType<AreusShockwave>(),
                30 + areusEnergy, 0, Player.whoAmI);
            areusEnergy = 0;
            foreach (var projectile in projectiles)
            {
                projectile.DamageType = DamageClass.Melee;
                projectile.timeLeft = 15;
            }
            SoundEngine.PlaySound(SoundID.Item38, Player.Center);
        }
        private void GuardArmor_Ranged()
        {
            if (!Player.HasBuff<ElectricMarksman>())
            {
                Player.AddBuff<ElectricMarksman>(18000);
            }
        }
        private void GuardArmor_Magic()
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
        private void GuardArmor_Summon()
        {
            if (areusEnergy >= AREUS_ENERGY_MAX)
            {
                if (!Player.HasBuff<ChargedMinions>())
                {
                    Player.AddBuff<ChargedMinions>(600);
                }
            }
        }

        public override void UpdateEquips()
        {
            if (Player.HasItemEquipped<GuardLeggings>(out var _))
            {
                Player.moveSpeed += 0.08f;
            }
            if (Player.HasItemEquipped<SoldierLeggings>(out var _))
            {
                Player.moveSpeed += 0.1f;
            }
            if (Player.HasItemEquipped<ImperialGreaves>(out var _))
            {
                Player.moveSpeed += 0.12f;
            }
            if (Player.HasItemEquipped<RoyalGreaves>(out var _))
            {
                Player.moveSpeed += 0.15f;
            }
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
    }
}
