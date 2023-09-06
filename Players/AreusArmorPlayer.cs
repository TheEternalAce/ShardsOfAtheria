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

        public bool guardSet;
        public bool soldierSet;
        public bool imperialSet;
        public bool royalSet;

        public int areusEnergy;
        public const int AreusEnergyMax = 100;
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
            guardSet = false;
            soldierSet = false;
            imperialSet = false;
            royalSet = false;
            areusDamage = 0f;
            if (areusEnergy > AreusEnergyMax)
            {
                areusEnergy = AreusEnergyMax;
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

                if (SoA.ArmorSetBonusActive.JustReleased)
                {
                    if (abilityTimer == 0)
                    {
                        if (guardSet)
                        {
                            if (areusEnergy > 0)
                            {
                                if (classChip == DamageClass.Melee)
                                {
                                    var projectiles = ShardsHelpers.ProjectileRing(Player.GetSource_FromThis(),
                                        Player.Center, 8, 1, 12f, ModContent.ProjectileType<AreusShockwave>(),
                                        30 + areusEnergy, 0, Player.whoAmI);
                                    areusEnergy = 0;
                                    foreach (var projectile in projectiles)
                                    {
                                        projectile.DamageType = DamageClass.Melee;
                                    }
                                    SoundEngine.PlaySound(SoundID.Item38, Player.Center);
                                }
                                if (classChip == DamageClass.Magic)
                                {
                                }
                            }
                        }
                        abilityTimer = AbilityTimerMax;
                    }
                }
            }
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            if (guardSet && classChip == DamageClass.Melee)
            {
                areusEnergy++;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (guardSet && classChip == DamageClass.Magic)
            {
                areusEnergy++;
            }
        }
    }
}
