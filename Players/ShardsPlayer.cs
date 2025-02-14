using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.PlayerDebuff;
using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.BuffItems;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Items.Tools.Misc.Slayer;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Projectiles.Melee.GenesisRagnarok;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class ShardsPlayer : ModPlayer
    {
        public bool baseConservation;
        public bool areusKey;
        public bool heartBreak;
        public bool healingItem;
        public bool phaseOffense;
        public bool rushDrive;
        public bool areusProcessor;
        public bool areusProcessorPrevious;
        public int processorElement = 0;
        public bool resonator;
        public bool areusRod;
        public bool acidTrip;
        public bool powerTrip;
        public bool disguiseCloak;
        public bool spoonBender;
        public int spoonBenderCD;
        public bool jumperCable;
        public int cableCounter;
        public bool destinyLance;
        public bool areusNullField;
        public bool hallowedSeal;
        public int hallowedSealCooldown;
        public bool HallowedSeal => hallowedSeal && hallowedSealCooldown <= 0;

        public bool valkyrieCrown;
        public bool hardlightBraces;
        public int hardlightBracesCooldown;
        public int hardlightBracesCooldownMax = 60;

        public bool prototypeBand;
        public int prototypeBandCooldown;
        public int prototypeBandCooldownMax = 15;

        public bool deathAmulet;
        public int deathAmuletCharges;
        public int deathInevitibility;

        public bool pearlwoodSet;
        public int pearlwoodBowShoot;

        public int combatTimer;
        public bool InCombat => combatTimer > 0;
        /// <summary>
        /// When above 0, you are in a combo. Ticks down by 1 every player update.
        /// <para>Item "combos" are used for determining what type of item action to use.</para>
        /// <para>A usage example would be a weapon with a 3 swing pattern. Each swing will increase the combo meter by 60, and when it becomes greater than 120, reset to 0.</para>
        /// </summary>
        public ushort itemCombo;

        public bool Biometal;
        public bool BiometalPrevious;
        public bool BiometalSound;
        public bool BiometalHideVanity;
        public bool BiometalForceVanity;
        public int overdriveTimeCurrent;
        public int overdriveTimer;
        public const int OVERDRIVE_TIME_MAX = 300;
        public bool Overdrive => Player.HasBuff<Overdrive>() && overdriveTimeCurrent > 0;

        public int riggedCoin;
        public int weightDie;

        public int readingDisk = 0;

        public int aggression = 0;

        public bool conductive;
        public int genesisRagnarockUpgrades = 0;
        public bool showRagnarok;
        public bool deathCloak;
        public bool DeathCloakCooldown => Player.HasBuff<MidnightCooldown>();

        public int[] strikeNPCs = new int[3];
        public int strikeCycle = 0;

        public bool sacrificedKatana = false;
        public int katanaTransformTimer;

        public bool ArmorSetCooldown => Player.ArmorSetCooldown();

        public float shiftTooltipCycleTimer = 0f;
        public int shiftTooltipIndex = 0;

        public override void ResetEffects()
        {
            baseConservation = false;
            areusKey = false;
            heartBreak = false;
            healingItem = false;
            rushDrive = false;
            valkyrieCrown = false;
            showRagnarok = false;
            hardlightBraces = false;
            if (hardlightBracesCooldown > 0) hardlightBracesCooldown--;
            prototypeBand = false;
            if (prototypeBandCooldown > 0) prototypeBandCooldown--;
            pearlwoodSet = false;
            areusProcessorPrevious = areusProcessor;
            areusProcessor = false;
            acidTrip = false;
            powerTrip = false;
            resonator = false;
            disguiseCloak = false;
            spoonBender = false;
            if (spoonBenderCD > 0) spoonBenderCD--;
            if (!jumperCable) cableCounter = 0;
            jumperCable = false;
            destinyLance = false;
            riggedCoin = 0;
            weightDie = 0;
            areusNullField = false;
            deathAmulet = false;
            hallowedSeal = false;
            if (hallowedSealCooldown > 0) hallowedSealCooldown--;

            BiometalPrevious = Biometal;
            Biometal = BiometalHideVanity = BiometalForceVanity = false;

            UpdateResource();

            if (combatTimer > 0) combatTimer--;
            else if (aggression > 0) aggression--;
            if (aggression >= 100) aggression = 100;
            else if (aggression < 0) aggression = 0;

            conductive = false;
            deathCloak = false;

            if (NPC.downedPlantBoss && sacrificedKatana) katanaTransformTimer++;
        }

        public override void Initialize()
        {
            overdriveTimeCurrent = OVERDRIVE_TIME_MAX;
            itemCombo = 0;
            phaseOffense = true;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["overdriveTimeCurrent"] = overdriveTimeCurrent;
            tag["phaseOffense"] = phaseOffense;
            tag[nameof(areusRod)] = areusRod;
            tag[nameof(genesisRagnarockUpgrades)] = genesisRagnarockUpgrades;
            tag[nameof(sacrificedKatana)] = sacrificedKatana;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("overdriveTimeCurrent"))
                overdriveTimeCurrent = (int)tag["overdriveTimeCurrent"];
            if (tag.ContainsKey("phaseOffense"))
                phaseOffense = tag.GetBool("phaseOffense");
            if (tag.ContainsKey(nameof(areusRod)))
                areusRod = tag.GetBool(nameof(areusRod));
            if (tag.ContainsKey(nameof(genesisRagnarockUpgrades)))
                genesisRagnarockUpgrades = tag.GetInt(nameof(genesisRagnarockUpgrades));
            if (tag.ContainsKey(nameof(sacrificedKatana)))
                sacrificedKatana = tag.GetBool(nameof(sacrificedKatana));
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            List<Item> items = [];
            if (Player.name.StartsWith("Gamma"))
            {
                items.Add(new Item(ModContent.ItemType<PlagueRailgun>()));
                items.Add(new Item(ModContent.ItemType<PlagueHandgun>()));
            }
            if (Player.name.StartsWith("Theta"))
            {
                items.Add(new Item(ModContent.ItemType<TwinFlameSwords>()));
                items.Add(new Item(ModContent.ItemType<FlameKnuckleBuster>()));
            }
            if (Player.name.StartsWith("Data"))
            {
                items.Add(new Item(ModContent.ItemType<DataTome>()));
                items.Add(new Item(ModContent.ItemType<DataKnife>()));
            }
            if (!mediumCoreDeath)
            {
                items.Add(new Item(ModContent.ItemType<Necronomicon>()));
            }
            if (Player.GetModPlayer<SinfulPlayer>().SinfulSoulUsed == 0)
            {
                items.Add(new Item(ModContent.ItemType<SinfulSoul>()));
            }

            return items;
        }

        public override void PostUpdate()
        {
            if (!Biometal)
            {
                Player.ClearBuff(ModContent.BuffType<Overdrive>());
                Player.buffImmune[ModContent.BuffType<Overdrive>()] = true;
            }
            else
            {
                Player.buffImmune[ModContent.BuffType<Overdrive>()] = false;
            }
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] > 0)
            {
                Player.statDefense += 20;
                Player.endurance += .15f;
            }
            TryAreusKatanaTransformation();
        }

        private void TryAreusKatanaTransformation()
        {
            if (++katanaTransformTimer >= 600 + Main.rand.Next(300))
            {
                katanaTransformTimer = 0;
                if (Player.HasItem(ModContent.ItemType<AreusKatana>()) &&
                    NPC.downedPlantBoss && sacrificedKatana)
                {
                    var pos = Player.Center + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 75;
                    var vel = Player.Center - pos;
                    vel.Normalize();
                    vel *= 16f;
                    Projectile.NewProjectile(Player.GetSource_FromThis(), pos, vel,
                        ModContent.ProjectileType<InfernalKatana>(), 1, 0, Player.whoAmI);
                }
            }
        }

        public override void PostUpdateBuffs()
        {
            if (Biometal)
            {
                Player.moveSpeed += .1f;
            }
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                Player.moveSpeed += .5f;
            }
            if (Player.HasBuff(ModContent.BuffType<SoulInfused>()))
            {
                Player.moveSpeed += .5f;
            }
            Player.GetDamage(DamageClass.Generic) += 0.01f * aggression;
            Player.moveSpeed += 0.01f * aggression;
            Player.aggro += aggression;
        }

        public void UpdateItemFields()
        {
            if (itemCombo > 0)
            {
                itemCombo--;
            }
        }

        public override void UpdateEquips()
        {
            if (rushDrive)
            {
                if (Player.statLife < Player.statLifeMax2 / 2)
                {
                    if (phaseOffense)
                    {
                        Player.GetDamage(DamageClass.Generic) += 1f;
                        Player.GetCritChance(DamageClass.Generic) += 0.05f;
                        Player.statDefense /= 2;
                    }
                    else
                    {
                        Player.GetDamage(DamageClass.Generic) -= 0.5f;
                        Player.endurance += 0.2f;
                        Player.statDefense *= 2;
                    }
                    Player.moveSpeed += .2f;
                }
            }
            if (Player.HeldItem.type == ModContent.ItemType<AreusKatana>())
            {
                Player.moveSpeed += .05f;
            }
        }

        public override void PreUpdateMovement()
        {
            if (Player.HasItem<SpeedCapper>())
            {
                var speedLimit = ToggleableTool.GetInstance<SpeedCapper>(Player);
                if (speedLimit != null && speedLimit.Active)
                {
                    float playerHorizontalSpeed = Player.velocity.X;
                    float maxHorizontalSpeed = SoA.ClientConfig.speedLimit * (42240f / 216000f);
                    if (playerHorizontalSpeed > maxHorizontalSpeed)
                    {
                        Player.velocity.X = maxHorizontalSpeed;
                    }
                }
            }
        }

        public override void UpdateLifeRegen()
        {
            if (areusNullField)
            {
                Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                return;
            }
            if (Biometal && !Player.HasBuff(ModContent.BuffType<Overdrive>()))
                Player.lifeRegen += 4;
            if (Player.HasBuff(ModContent.BuffType<SoulInfused>()))
                Player.lifeRegen += 4;
            if (areusKey)
                Player.lifeRegen *= 2;
        }

        private void UpdateResource()
        {
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                // For our resource lets make it regen slowly over time to keep it simple, let's use exampleResourceRegenTimer to count up to whatever value we want, then increase currentResource.
                overdriveTimer++; //Increase it by 60 per second, or 1 per tick.

                // A simple timer that goes up to 1 seconds, increases the overdriveTime by 1 and then resets back to 0.
                if (overdriveTimer > 60)
                {
                    overdriveTimeCurrent -= 1;
                    overdriveTimer = 0;
                }
            }
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (baseConservation)
                return Main.rand.NextFloat() >= .15f;
            return true;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.keyState.IsKeyDown(Keys.Escape))
            {
                readingDisk = 0;
                ModContent.GetInstance<UpgradeUISystem>().HideUI();
            }
            if (SoA.OverdriveKey.JustPressed)
            {
                if (Biometal)
                {
                    if (overdriveTimeCurrent > 0 && !Player.HasBuff(ModContent.BuffType<Overdrive>()))
                    {
                        Player.AddBuff(ModContent.BuffType<Overdrive>(), 2);
                        CombatText.NewText(Player.Hitbox, Color.Green, "Overdrive: ON", true);
                        SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                    }
                    else
                    {
                        Player.ClearBuff(ModContent.BuffType<Overdrive>());
                        CombatText.NewText(Player.Hitbox, Color.Red, "Overdrive: OFF");
                    }
                }
            }
            if (SoA.ProcessorElement != null && SoA.ProcessorElement.JustPressed)
            {
                if (areusProcessor)
                {
                    if (++processorElement > 3)
                    {
                        processorElement = 0;
                    }
                }
            }
            if (SoA.PhaseSwitch.JustPressed)
            {
                if (Player.statLife >= Player.statLifeMax2 / 2 && rushDrive)
                {
                    if (phaseOffense)
                    {
                        phaseOffense = false;
                        ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("Phase 2 Type: Defensive"), Color.White, Player.whoAmI);
                    }
                    else
                    {
                        phaseOffense = true;
                        ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("Phase 2 Type: Offensive"), Color.White, Player.whoAmI);
                    }
                }
            }
        }

        public override bool CanHitPvp(Item item, Player target)
        {
            if (areusNullField || target.Shards().areusNullField) return false;
            return base.CanHitPvp(item, target);
        }

        public override bool CanHitPvpWithProj(Projectile proj, Player target)
        {
            if (areusNullField || target.Shards().areusNullField) return false;
            return base.CanHitPvpWithProj(proj, target);
        }

        public override bool CanHitNPC(NPC target)
        {
            if (areusNullField) return false;
            return base.CanHitNPC(target);
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (SoA.BNEEnabled) modifiers.FinalDamage *= ResonatorRing.ModifyElements(Player, item, target);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (SoA.BNEEnabled) modifiers.FinalDamage *= ResonatorRing.ModifyElements(Player, proj, target);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            bool killingBlow = false;
            if (target.life <= 0) killingBlow = true;
            if (killingBlow)
            {
                if (deathAmulet && deathAmuletCharges < 10) deathAmuletCharges++;
            }
            if (target.lifeMax > 5) combatTimer = 300;
            if (valkyrieCrown) target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
            HardlightBraces.OnHitEffect(Player, target);
            if (jumperCable)
            {
                int maxCounter = 10;
                if (ShardsHelpers.AnyBosses())
                {
                    maxCounter += 10;
                    cableCounter++;
                }
                else if (killingBlow) cableCounter++;
                if (cableCounter >= maxCounter && (!ShardsHelpers.AnyBosses() || Main.rand.NextBool(5)))
                {
                    if (Player.HasBuff<ClockCooldown>()) Player.ClearBuff<ClockCooldown>();
                    if (Player.HasBuff<EmeraldTeleportCooldown>()) Player.ClearBuff<EmeraldTeleportCooldown>();
                    if (Player.HasBuff<HeartBreak>()) Player.ClearBuff<HeartBreak>();
                    if (Player.HasBuff<SetBonusCooldown>()) Player.ClearBuff<SetBonusCooldown>();
                    if (Player.HasBuff<SoulTeleportCooldown>()) Player.ClearBuff<SoulTeleportCooldown>();
                    if (ModLoader.TryGetMod("GMR", out Mod gerdMod) && gerdMod.TryFind("InfraRedCorrosion", out ModBuff infraRedCooldown) && Player.HasBuff(infraRedCooldown.Type))
                        Player.ClearBuff(infraRedCooldown.Type);
                    hardlightBracesCooldown = 0;
                    prototypeBandCooldown = 0;
                    Player.Slayer().soulCrystalProjectileCooldown = 0;
                    cableCounter = 0;
                }
            }
            if (HallowedSeal)
            {
                if (hit.DamageType.CountsAsClass(DamageClass.Melee))
                {
                    int mana = 9;
                    if (killingBlow) mana += 6;
                    Player.statMana += mana;
                    Player.ManaEffect(mana);
                    hallowedSealCooldown = Player.itemAnimation;
                }
            }
            if (Player.HasItem<TheMourningStar>())
            {
                int ind = Player.FindItem<TheMourningStar>();
                Item item = Player.inventory[ind];
                var sword = item.ModItem as TheMourningStar;
                if (Player.Distance(target.Center) < 260f)
                {
                    int feedSword = 60;
                    if (Player.Distance(target.Center) < 130) feedSword += 60;
                    if (killingBlow) feedSword += 60;
                    if (Player.HeldItem.type == item.type) feedSword *= 2;
                    sword.blood += feedSword;
                }
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SoA.BNEEnabled) AreusRodEffect(target, item);
            var chip = ToggleableTool.GetInstance<AnchorChip>(Player);
            if (hallowedSeal && item.DamageType == DamageClass.Melee && (chip == null || !chip.Active))
            {
                var vector = Player.Center - target.Center;
                vector.Normalize();
                Player.velocity = vector * 5f;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SoA.BNEEnabled) AreusRodEffect(target, proj);
            var chip = ToggleableTool.GetInstance<AnchorChip>(Player);
            if (hallowedSeal && proj.IsTrueMelee() && (chip == null || !chip.Active))
            {
                var vector = Player.Center - target.Center;
                vector.Normalize();
                Player.velocity = vector * 5f;
            }
        }

        [JITWhenModsEnabled("BattleNetworkElements")]
        private void AreusRodEffect(NPC target, object source)
        {
            if (areusRod)
            {
                int electricDebuff = ModContent.BuffType<ElectricShock>();
                int debuffTime = 600;
                bool inflictDebuff = false;
                if (source is Item item && item.IsElec()) inflictDebuff = true;
                if (source is Projectile proj && proj.IsElec()) inflictDebuff = true;
                if (NPC.downedPlantBoss) electricDebuff = BuffID.Electrified;
                if (conductive) debuffTime *= 2;
                if (inflictDebuff) target.AddBuff(electricDebuff, debuffTime);
            }
        }

        public override void UpdateVisibleVanityAccessories()
        {
            for (int n = 13; n < 18 + Player.GetAmountOfExtraAccessorySlotsToShow(); n++)
            {
                Item item = Player.armor[n];
                if (item.type == ModContent.ItemType<Biometal>())
                {
                    BiometalHideVanity = false;
                    BiometalForceVanity = true;
                }
            }
        }

        public override void FrameEffects()
        {
            if ((Biometal || BiometalForceVanity) && !BiometalHideVanity)
            {
                var biometal = ModContent.GetInstance<Biometal>();
                Player.head = EquipLoader.GetEquipSlot(Mod, biometal.Name, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, biometal.Name, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, biometal.Name, EquipType.Legs);

                Player.handon = -1;
                Player.handoff = -1;
                Player.back = -1;
                Player.front = -1;
                Player.shoe = -1;
                Player.waist = -1;
                Player.neck = -1;
                Player.face = -1;
                Player.balloon = -1;
            }
            if (Overdrive)
            {
                Player.armorEffectDrawShadow = true;
                Player.armorEffectDrawOutlines = true;
            }
            if (showRagnarok && Player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] == 0
                && Player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] == 0
                && Player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] == 0)
            {
                var ragnarok = ModContent.GetInstance<GenesisAndRagnarok>();
                Player.shield = (sbyte)EquipLoader.GetEquipSlot(Mod, ragnarok.Name, EquipType.Shield);
            }
        }

        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (disguiseCloak && !Player.HasBuff<DisguiseRegenerating>())
            {
                Player.AddBuff<DisguiseRegenerating>(18000);
                Player.SetImmuneTimeForAllTypes(60);
                CombatText.NewText(Player.Hitbox, Color.White, "Disguse busted!", true);
                return true;
            }
            return base.ConsumableDodge(info);
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    Main.npc[i].GetGlobalNPC<SoAGlobalNPC>().flawless = false;
                }
            }
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            combatTimer = 300;
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
                Player.ClearBuff(ModContent.BuffType<Overdrive>());
            if (healingItem && !heartBreak)
            {
                Player.AddBuff(ModContent.BuffType<HeartBreak>(), 900);
            }
            if (powerTrip)
            {
                Item trip = ModContent.GetInstance<AcidTrip>().Item;
                switch (processorElement)
                {
                    case 0:
                        TripEffect(trip, ProjectileID.Flames);
                        break;
                    case 1:
                        TripEffect(trip, ProjectileID.BallofFrost);
                        break;
                    case 2:
                        TripEffect(trip, ProjectileID.ThunderStaffShot);
                        break;
                    case 3:
                        TripEffect(trip, ProjectileID.ToxicCloud);
                        break;
                }
            }
            if (acidTrip)
            {
                Item trip = ModContent.GetInstance<AcidTrip>().Item;
                TripEffect(trip, ProjectileID.ToxicCloud);
            }
        }

        void TripEffect(Item trip, int projType)
        {
            if (projType == ProjectileID.ToxicCloud)
            {
                projType += Main.rand.Next(3);
            }
            for (int i = 0; i < 8; i++)
            {
                var vector = Vector2.One * 8 * Main.rand.NextFloat();
                Projectile.NewProjectile(Player.GetSource_Accessory(trip), Player.Center,
                    vector.RotatedByRandom(MathHelper.TwoPi), projType,
                    Player.GetWeaponDamage(trip), Player.GetWeaponKnockback(trip), Player.whoAmI);
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (damageSource.SourceNPCIndex == -1 &&
                damageSource.SourceItem == null &&
                damageSource.SourceProjectileType == 0)
            {
                if (Player.HasBuff(ModContent.BuffType<DeathBleed>())) damageSource = PlayerDeathReason.ByCustomReason(Player.name + " bled out.");
                if (Player.HasBuff(ModContent.BuffType<CorruptedBlood>()))
                {
                    string[] deathSuffix = [
                        " was exsanguinated by The Mourning Star.",
                        " was drained by The Mourning Star.",
                        " let The Mourning Star eat their blood."
                        ];
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + deathSuffix[^1]);
                }
            }
            if (deathAmulet && deathAmuletCharges > 0 && deathInevitibility < 10)
            {
                deathAmuletCharges--;
                deathInevitibility++;
                Player.Heal(Player.statLifeMax2);
                Player.SetImmuneTimeForAllTypes(30);
                Player.AddBuff<MementoMori>(2);
                return false;
            }
            if (deathCloak && !DeathCloakCooldown)
            {
                Player.AddBuff<MidnightCooldown>(5 * 60 * 60);
                Player.Heal(Player.statLifeMax2 / 2);
                Vector2 teleport = Vector2.Zero;
                bool validTeleport = false;
                while (!validTeleport)
                {
                    teleport = Player.Center + Vector2.One.RotateRandom(MathHelper.TwoPi) * Main.rand.NextFloat(100, 200);
                    validTeleport = ShardsHelpers.CheckTileCollision(teleport, Player.Hitbox);
                }
                Player.Teleport(teleport, 1);
                NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, teleport.X, teleport.Y, 1);
                return false;
            }
            return true;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            deathInevitibility = 0;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Player.HasBuff(ModContent.BuffType<InjectionShock>()) || Player.HasBuff(ModContent.BuffType<CorruptedBlood>()))
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    var dust = Dust.NewDustDirect(Player.position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Blood, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f);
                    dust.noGravity = true;
                    dust.velocity *= 1.8f;
                    dust.velocity.Y -= 0.5f;
                }
            }
        }
    }
}