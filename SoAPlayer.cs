using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.Weapons;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria
{
    public class SoAPlayer : ModPlayer
    {
        public bool areusBatteryElectrify;
        public bool areusWings;
        public bool lesserSapphireCore;
        public bool sapphireCore;
        public bool superSapphireCore;
        public bool greaterRubyCore;
        public bool superRubyCore;
        public bool omnicientTome;
        public bool baseConservation;
        public bool sapphireMinion;
        public bool lesserEmeraldCore;
        public bool emeraldCore;
        public bool superEmeraldCore;
        public bool areusKey;
        public bool megaGemCore;
        public bool shadowBrand;
        public bool shadowBrandToggled;
        public bool hallowedSeal;
        public bool zenovaJavelin;
        public bool heartBreak;
        public bool sMHealingItem;
        public bool phaseOffense;
        public bool rushDrive;
        public bool markOfAnastasia;
        public bool areusChargePack;
        public bool valkyrieCrown;

        public bool inCombat;
        public int inCombatTimer;

        //Slayer mode stuff
        public bool creeperPet;
        public bool honeyCrown;
        public bool honeybeeMinion;
        public bool vampiricJaw;
        public bool plantCells;
        public bool moonCore;
        public bool spiderClock;
            //Spider Clock
        public Vector2 recentPos;
        public int recentLife;
        public int recentMana;
        public int recentCharge;
        public int saveTimer;

        public int TomeKnowledge;

        public bool naturalAreusRegen;
        public bool areusChargeMaxed;

        // These 5 relate to ExampleCostume.
        public bool BiometalPrevious;
        public bool Biometal;
        public bool BiometalHideVanity;
        public bool BiometalForceVanity;

        public int overdriveTimeCurrent;
        public const int DefaultOverdriveTimeMax = 300;
        public int overdriveTimeMax;
        public int overdriveTimeMax2;
        internal int overdriveTimeRegenTimer = 0;

        public override void ResetEffects()
       {
            areusBatteryElectrify = false;
            areusWings = false;
            lesserSapphireCore = false;
            lesserEmeraldCore = false;
            sapphireCore = false;
            superSapphireCore = false;
            greaterRubyCore = false;
            superRubyCore = false;
            omnicientTome = false;
            baseConservation = false;
            sapphireMinion = false;
            superEmeraldCore = false;
            areusKey = false;
            megaGemCore = false;
            shadowBrand = false;
            hallowedSeal = false;
            zenovaJavelin = false;
            heartBreak = false;
            sMHealingItem = false;
            rushDrive = false;
            valkyrieCrown = false;
            markOfAnastasia = false;

            inCombat = false;

            //Slayer mode stuff
            creeperPet = false;
            honeyCrown = false;
            honeybeeMinion = false;
            vampiricJaw = false;
            plantCells = false;
            moonCore = false;
            spiderClock = false;

            ResetVariables();
            naturalAreusRegen = false;

            BiometalPrevious = Biometal;
            Biometal = BiometalHideVanity = BiometalForceVanity = false;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            overdriveTimeMax2 = overdriveTimeMax;
        }

        public override void Initialize()
        {
            overdriveTimeCurrent = 300;
            overdriveTimeMax = DefaultOverdriveTimeMax;

            TomeKnowledge = 0;
            shadowBrandToggled = false;
            phaseOffense = true;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["TomeKnowledge"] = TomeKnowledge;
            tag["shadowBrandToggled"] = shadowBrandToggled;
            tag["overdriveTimeCurrent"] = overdriveTimeCurrent;
            tag["phaseOffense"] = phaseOffense;
        }

        public override void LoadData(TagCompound tag)
        {
            TomeKnowledge = (int)tag["TomeKnowledge"];
            shadowBrandToggled = tag.GetBool("shadowBrandToggled");
            overdriveTimeCurrent = (int)tag["overdriveTimeCurrent"];
            phaseOffense = tag.GetBool("phaseOffense");
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new Item[] {
                new Item(ModContent.ItemType<SlayersEmblem>()),
                new Item(ModContent.ItemType<MicrobeAnalyzer>())
            };
        }

        public override void PreUpdate()
        {
            UpdateResource();
            if (omnicientTome)
            {
                if (TomeKnowledge == 0)
                {
                    Player.AddBuff(ModContent.BuffType<BaseCombat>(), 2);
                }
                else if (TomeKnowledge == 1)
                {
                    Player.AddBuff(ModContent.BuffType<BaseConservation>(), 2);
                }
                else if (TomeKnowledge == 2)
                {
                    Player.AddBuff(ModContent.BuffType<BaseExploration>(), 2);
                    Player.AddBuff(BuffID.Mining, 2);
                    Player.AddBuff(BuffID.Builder, 2);
                    Player.AddBuff(BuffID.Hunter, 2);
                    Player.AddBuff(BuffID.Spelunker, 2);
                }
            }

            if (megaGemCore || areusWings)
            {
                Player.wingTime = 100;
                Player.rocketTime = 100;
            }
            if (spiderClock)
            {
                saveTimer++;
                if (saveTimer == 300)
                {
                    recentPos = Player.position;
                    recentLife = Player.statLife;
                    recentMana = Player.statMana;
                    CombatText.NewText(Player.Hitbox, Color.Gray, "Time shift ready");
                }
                if (saveTimer >= 302)
                    saveTimer = 302;
            }
            else saveTimer = 0;
            if (ModContent.GetInstance<SoAWorld>().messageToPlayer > 1080)
                Player.AddBuff(ModContent.BuffType<AwakenedSlayer>(), 18000);
        }

        public override void PostUpdate()
        {
            if (!Player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                Player.ClearBuff(ModContent.BuffType<Overdrive>());
                Player.buffImmune[ModContent.BuffType<Overdrive>()] = true;
            }
            else Player.buffImmune[ModContent.BuffType<Overdrive>()] = false;
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] > 0)
            {
                Player.statDefense += 20;
                Player.endurance += .15f;
            }
        }

        public override void PreUpdateBuffs()
        {
            if (inCombatTimer > 0)
            {
                inCombatTimer--;
                inCombat = true;
            }
        }

        public override void PostUpdateBuffs()
        {
            if (Player.HasBuff(ModContent.BuffType<BaseExploration>()))
            {
                Player.moveSpeed += .1f;
            }
            if (Player.HasBuff(ModContent.BuffType<Megamerged>()))
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
        }

        public override void UpdateEquips()
        {
            if (areusKey)
            {
                Player.moveSpeed += .5f;
            }
            if (lesserEmeraldCore)
            {
                Player.moveSpeed += .05f;
            }
            if (emeraldCore)
            {
                Player.moveSpeed += .1f;
            }
            if (superEmeraldCore)
            {
                Player.moveSpeed += .15f;
            }
            if (megaGemCore)
            {
                Player.moveSpeed += .2f;
            }
            if (markOfAnastasia)
            {
                if (Player.name == "Sophie" || Player.name == "Lilly" || Player.name == "Damien" || Player.name == "Ariiannah" || Player.name == "Arii" || Player.name == "Peter" || Player.name == "Shane")
                {
                    Player.moveSpeed += 1f;
                }
                else
                {
                    Player.moveSpeed += .5f;
                }
            }
            if (Player.statLife <= Player.statLifeMax2 / 2 && rushDrive)
            {
                Player.moveSpeed += .2f;
            }
            if (Player.HeldItem.type == ModContent.ItemType<AreusKatana>())
            {
                Player.moveSpeed += .05f;
            }
        }

        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<Megamerged>()) && !Player.HasBuff(ModContent.BuffType<Overdrive>()))
                Player.lifeRegen += 4;
            if (Player.HasBuff(ModContent.BuffType<SoulInfused>()))
                Player.lifeRegen += 4;
            if (areusKey)
                Player.lifeRegen *= 2;
            if (plantCells)
                if (Player.ZoneOverworldHeight)
                    Player.lifeRegen += 15;
                else Player.lifeRegen += 10;
        }

        private void UpdateResource()
        {
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()) && !Player.GetModPlayer<DecaPlayer>().modelDeca)
            {
                // For our resource lets make it regen slowly over time to keep it simple, let's use exampleResourceRegenTimer to count up to whatever value we want, then increase currentResource.
                overdriveTimeRegenTimer++; //Increase it by 60 per second, or 1 per tick.

                // A simple timer that goes up to 1 seconds, increases the overdriveTime by 1 and then resets back to 0.
                if (overdriveTimeRegenTimer > 60)
                {
                    overdriveTimeCurrent -= 1;
                    overdriveTimeRegenTimer = 0;
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
            if (ShardsOfAtheria.QuickCharge.JustPressed)
            {
                if (Player.HeldItem.ModItem is AreusWeapon && Player.HasItem(ModContent.ItemType<AreusChargePack>()))
                {
                    if ((Player.HeldItem.ModItem as AreusWeapon).areusCharge < (Player.HeldItem.ModItem as AreusWeapon).areusChargeFull)
                    {
                        int areusChargePackIndex = Player.FindItem(ModContent.ItemType<AreusChargePack>());

                        Main.LocalPlayer.inventory[areusChargePackIndex].stack--;
                        (Player.HeldItem.ModItem as AreusWeapon).areusCharge += 50;
                        SoundEngine.PlaySound(SoundID.NPCHit53);
                        CombatText.NewText(Player.Hitbox, Color.Aqua, 50);
                    }
                }
            }
            if (ShardsOfAtheria.OverdriveKey.JustPressed)
            {
                if (Player.HasBuff(ModContent.BuffType<Megamerged>()) && !Player.HasBuff(ModContent.BuffType<Overdrive>()) && overdriveTimeCurrent >= 0)
                {
                    Player.AddBuff(ModContent.BuffType<Overdrive>(), 2);
                    CombatText.NewText(Player.Hitbox, Color.Green, "Overdrive: ON", true);
                    SoundEngine.PlaySound(SoundID.Item4, Player.position);
                }
                else if (Player.HasBuff(ModContent.BuffType<Megamerged>()))
                {
                    Player.ClearBuff(ModContent.BuffType<Overdrive>());
                    CombatText.NewText(Player.Hitbox, Color.Red, "Overdrive: OFF");
                }
            }
            if (ShardsOfAtheria.TomeKey.JustPressed)
            {
                if (omnicientTome)
                {
                    if (TomeKnowledge == 2)
                    {
                        TomeKnowledge = 0;
                    }
                    else TomeKnowledge += 1;
                    SoundEngine.PlaySound(SoundID.Item1, Player.position);
                }
            }
            if (ShardsOfAtheria.EmeraldTeleportKey.JustPressed)
            {
                if (superEmeraldCore)
                {
                    Vector2 vector21 = default(Vector2);
                    vector21.X = (float)Main.mouseX + Main.screenPosition.X;
                    if (Player.gravDir == 1f)
                    {
                        vector21.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)Player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                    }
                    vector21.X -= Player.width / 2;
                    if (vector21.X > 50f && vector21.X < (float)(Main.maxTilesX * 16 - 50) && vector21.Y > 50f && vector21.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].WallType != 87 || !((double)num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, Player.width, Player.height))
                        {
                            Player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, vector21.X, vector21.Y, 1);
                            if (Player.chaosState)
                            {
                                Player.statLife -= Player.statLifeMax2 / 7;
                                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                                if (Main.rand.Next(2) == 0)
                                {
                                    damageSource = PlayerDeathReason.ByOther(Player.Male ? 14 : 15);
                                }
                                if (Player.statLife <= 0)
                                {
                                    Player.KillMe(damageSource, 1.0, 0);
                                }
                                Player.lifeRegenCount = 0;
                                Player.lifeRegenTime = 0;
                            }
                            Player.AddBuff(BuffID.ChaosState, 360);
                        }
                    }
                }
                if (megaGemCore)
                {
                    Vector2 vector21 = default(Vector2);
                    vector21.X = (float)Main.mouseX + Main.screenPosition.X;
                    if (Player.gravDir == 1f)
                    {
                        vector21.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)Player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                    }
                    vector21.X -= Player.width / 2;
                    if (vector21.X > 50f && vector21.X < (float)(Main.maxTilesX * 16 - 50) && vector21.Y > 50f && vector21.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].WallType != 87 || !((double)num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, Player.width, Player.height))
                        {
                            Player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, vector21.X, vector21.Y, 1);
                            if (Player.chaosState)
                            {
                                Player.statLife -= Player.statLifeMax2 / 7;
                                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                                if (Main.rand.Next(2) == 0)
                                {
                                    damageSource = PlayerDeathReason.ByOther(Player.Male ? 14 : 15);
                                }
                                if (Player.statLife <= 0)
                                {
                                    Player.KillMe(damageSource, 1.0, 0);
                                }
                                Player.lifeRegenCount = 0;
                                Player.lifeRegenTime = 0;
                            }
                            Player.AddBuff(BuffID.ChaosState, 360);
                        }
                    }
                }
            }
            if (ShardsOfAtheria.ShadowCloak.JustPressed)
            {
                if (shadowBrand)
                {
                    if (shadowBrandToggled)
                    {
                        shadowBrandToggled = false;
                    }
                    else shadowBrandToggled = true;
                }
            }
            if (ShardsOfAtheria.ShadowTeleport.JustPressed)
            {
                if (shadowBrand && Player.HasBuff(ModContent.BuffType<ShadowTeleport>()))
                {
                    Vector2 vector21 = default(Vector2);
                    vector21.X = (float)Main.mouseX + Main.screenPosition.X;
                    if (Player.gravDir == 1f)
                    {
                        vector21.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)Player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                    }
                    vector21.X -= Player.width / 2;
                    if (vector21.X > 50f && vector21.X < (float)(Main.maxTilesX * 16 - 50) && vector21.Y > 50f && vector21.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].WallType != 87 || !((double)num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, Player.width, Player.height))
                        {
                            Player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, vector21.X, vector21.Y, 1);
                            Player.ClearBuff(ModContent.BuffType<ShadowTeleport>());
                        }
                    }
                }
            }
            if (ShardsOfAtheria.PhaseSwitch.JustPressed)
            {
                if (Player.statLife >= Player.statLifeMax2 / 2 && rushDrive)
                {
                    if (phaseOffense)
                    {
                        phaseOffense = false;
                        Main.NewText("Phase 2 Type: Defensive");
                    }
                    else
                    {
                        phaseOffense = true;
                        Main.NewText("Phase 2 Type: Offensive");
                    }
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            inCombatTimer = 300;
            if (valkyrieCrown)
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
            if (areusBatteryElectrify)
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
            if (greaterRubyCore)
                target.AddBuff(BuffID.OnFire, 10 * 60);
            if (superRubyCore)
            {
                target.AddBuff(BuffID.CursedInferno, 10 * 60);
                target.AddBuff(BuffID.Ichor, 10 * 60);
            }
            if (megaGemCore)
            {
                target.AddBuff(BuffID.Daybreak, 10 * 60);
                target.AddBuff(BuffID.BetsysCurse, 10 * 60);
                Player.AddBuff(BuffID.Ironskin, 10 * 60);
                Player.AddBuff(BuffID.Endurance, 10 * 60);
            }
            if (hallowedSeal && item.DamageType == DamageClass.Melee)
                Player.statMana += 15;
            if (vampiricJaw && item.DamageType == DamageClass.Melee && !item.noMelee)
            {
                Player.HealEffect(item.damage / 5);
                Player.statLife += item.damage / 5;
            }
            if (moonCore)
            {
                Player.HealEffect(item.damage / 2);
                Player.statLife += item.damage / 2;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            inCombatTimer = 300;
            if (proj.owner == Player.whoAmI)
            {
                if (valkyrieCrown)
                    target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
                if (areusBatteryElectrify)
                    target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
                if (greaterRubyCore)
                    target.AddBuff(BuffID.OnFire, 10 * 60);
                if (superRubyCore)
                {
                    target.AddBuff(BuffID.CursedInferno, 10 * 60);
                    target.AddBuff(BuffID.Ichor, 10 * 60);
                }
                if (megaGemCore)
                {
                    target.AddBuff(BuffID.Daybreak, 10 * 60);
                    target.AddBuff(BuffID.BetsysCurse, 10 * 60);
                    Player.AddBuff(BuffID.Ironskin, 10 * 60);
                    Player.AddBuff(BuffID.Endurance, 10 * 60);
                }
                if (hallowedSeal && proj.DamageType == DamageClass.Melee)
                    Player.statMana += 15;
                if (moonCore)
                {
                    Player.HealEffect(proj.damage / 10);
                    Player.statLife += proj.damage / 10;
                }
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
                var exampleCostume = ModContent.GetInstance<Biometal>();
                Player.head = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Head);
                Player.body = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Body);
                Player.legs = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Legs);

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
        }

        //public override void UpdateEquips()
        //{
        //    if (Player.HeldItem.type == ModContent.ItemType<Ragnarok>() && Player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] < 1)
        //    {
        //        Player.hasRaisableShield = true;
        //    }
        //}

        //public override void UpdateVisibleAccessories()
        //{
        //    if (Player.HeldItem.type == ModContent.ItemType<Ragnarok>() && Player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] < 1)
        //    {
        //        Player.shield = 11;
        //        Player.cShield = 0;
        //    }
        //}

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (Player.HasBuff(ModContent.BuffType<Megamerged>()))
                SoundEngine.PlaySound(SoundID.NPCHit4, Player.position);
            if (!Player.immune)
            {
                if (Player.whoAmI == Main.myPlayer && lesserSapphireCore && Main.rand.NextFloat() < 0.05f)
                    Player.NinjaDodge();
                if (Player.whoAmI == Main.myPlayer && sapphireCore && Main.rand.NextFloat() < 0.1f)
                    Player.NinjaDodge();
                if (Player.whoAmI == Main.myPlayer && superSapphireCore && Main.rand.NextFloat() < 0.15f)
                    Player.NinjaDodge();
                if (Player.whoAmI == Main.myPlayer && megaGemCore && Main.rand.NextFloat() < 0.2f)
                    Player.NinjaDodge();
                if (Player.whoAmI == Main.myPlayer && shadowBrand && shadowBrandToggled && Main.rand.NextFloat() < .1f)
                    Player.NinjaDodge();
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            inCombatTimer = 300;
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
                Player.ClearBuff(ModContent.BuffType<Overdrive>());
            if (megaGemCore)
            {
                Player.AddBuff(BuffID.Rage, 10 * 60);
                Player.AddBuff(BuffID.Wrath, 10 * 60);
                Player.AddBuff(BuffID.Inferno, 10 * 60);
            }
            if (superSapphireCore)
                Player.AddBuff(BuffID.Inferno, 10 * 60);
            if (Player.HasItem(ModContent.ItemType<WandOfHealing>()) && !Player.HasBuff(ModContent.BuffType<HeartBreak>()))
                Player.AddBuff(ModContent.BuffType<HeartBreak>(), 10 * 60);
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " pushed too far.");
            }
            if (Player.HasBuff(ModContent.BuffType<MildRadiationPoisoning>()) || Player.HasBuff(ModContent.BuffType<ModerateRadiationPoisoning>()) || Player.HasBuff(ModContent.BuffType<SevereRadiationPoisoning>()))
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + "'s genes mutated fatally.");
            }
            return true;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Player.HasBuff(ModContent.BuffType<InjectionShock>()))
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Blood, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                }
            }
        }
    }
}