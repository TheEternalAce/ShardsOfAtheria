using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.SlayerItems.SlayersEquipment;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Tools;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace ShardsOfAtheria
{
    public class SlayerPlayer : ModPlayer
    {
        public int soulCrystals;

        public bool slayerSet;

        public bool KingSoul;
        public bool EyeSoul;
        public bool BrainSoul;
        public bool EaterSoul;
        public bool ValkyrieSoul;
        public bool BeeSoul;
        public bool SkullSoul;
        public bool DeerclopsSoul;
        public bool WallSoul;
        public bool QueenSoul;
        public bool DestroyerSoul;
        public bool TwinSoul;
        public bool PrimeSoul;
        public bool PlantSoul;
        public bool GolemSoul;
        public bool DukeSoul;
        public bool EmpressSoul;
        public bool LunaticSoul;
        public bool LordSoul;
        public bool LandSoul;
        public bool TimeSoul;
        public bool DeathSoul;

        public bool creeperPet;
        public bool vampiricJaw;
        public bool spiderClock;
        //Spider Clock
        public Vector2 recentPos;
        public int recentLife;
        public int recentMana;
        public int recentCharge;
        public int saveTimer;

        public int TomeKnowledge;
        public bool omnicientTome;
        public int creeperSpawnTimer;
        public int valkyrieDashTimer = 2;
        public int beeSpawnTimer;
        public int spinningTimer;
        public int theHungrySpawnTimer;
        public int yourTentacleSpawnTimer;
        public int soulTeleports;
        public int lunaticCircleFragments = 1;

        public int totalDamageTaken;
        public int lastDamageTaken;

        public int defenseReduction;
        public int defenseRegenTime;

        public int soulCrystalProjectileCooldown;

        public int selectedSoul;

        public override void ResetEffects()
        {
            creeperPet = false;
            vampiricJaw = false;
            spiderClock = false;
        }

        public override void Initialize()
        {
            soulCrystals = 0;

            slayerSet = false;

            KingSoul = false;
            EyeSoul = false;
            BrainSoul = false;
            EaterSoul = false;
            ValkyrieSoul = false;
            BeeSoul = false;
            SkullSoul = false;
            WallSoul = false;
            WallSoul = false;
            DestroyerSoul = false;
            DestroyerSoul = false;
            TwinSoul = false;
            PrimeSoul = false;
            PlantSoul = false;
            GolemSoul = false;
            DukeSoul = false;
            EmpressSoul = false;
            LunaticSoul = false;
            LordSoul = false;
            LandSoul = false;
            TimeSoul = false;
            DeathSoul = false;

            selectedSoul = SelectedSoul.None;
            TomeKnowledge = 0;
            omnicientTome = false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["soulCrystals"] = soulCrystals;

            tag["KingSoul"] = KingSoul;
            tag["EyeSoul"] = EyeSoul;
            tag["BrainSoul"] = BrainSoul;
            tag["EaterSoul"] = EaterSoul;
            tag["ValkyrieSoul"] = ValkyrieSoul;
            tag["BeeSoul"] = BeeSoul;
            tag["SkullSoul"] = SkullSoul;
            tag["DeerclopsSoul"] = DeerclopsSoul;
            tag["WallSoul"] = WallSoul;
            tag["QueenSoul"] = QueenSoul;
            tag["DestroyerSoul"] = DestroyerSoul;
            tag["TwinSoul"] = TwinSoul;
            tag["PrimeSoul"] = PrimeSoul;
            tag["PlantSoul"] = PlantSoul;
            tag["GolemSoul"] = GolemSoul;
            tag["DukeSoul"] = DukeSoul;
            tag["EmpressSoul"] = EmpressSoul;
            tag["LunaticSoul"] = LunaticSoul;
            tag["LordSoul"] = LordSoul;
            tag["LandSoul"] = LandSoul;
            tag["TimeSoul"] = TimeSoul;
            tag["DeathSoul"] = DeathSoul;

            tag["selectedSoul"] = selectedSoul;
            tag["selectedSoul"] = selectedSoul;
            tag["TomeKnowledge"] = TomeKnowledge;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("soulCrystals"))
                soulCrystals = tag.GetInt("soulCrystals");

            if (tag.ContainsKey("KingSoul"))
                KingSoul = tag.GetBool("KingSoul");
            if (tag.ContainsKey("EyeSoul"))
                EyeSoul = tag.GetBool("EyeSoul");
            if (tag.ContainsKey("BrainSoul"))
                BrainSoul = tag.GetBool("BrainSoul");
            if (tag.ContainsKey("EaterSoul"))
                EaterSoul = tag.GetBool("EaterSoul");
            if (tag.ContainsKey("ValkyrieSoul"))
                ValkyrieSoul = tag.GetBool("ValkyrieSoul");
            if (tag.ContainsKey("BeeSoul"))
                BeeSoul = tag.GetBool("BeeSoul");
            if (tag.ContainsKey("SkullSoul"))
                SkullSoul = tag.GetBool("SkullSoul");
            if (tag.ContainsKey("DeerclopsSoul"))
                DeerclopsSoul = tag.GetBool("DeerclopsSoul");
            if (tag.ContainsKey("WallSoul"))
                WallSoul = tag.GetBool("WallSoul");
            if (tag.ContainsKey("QueenSoul"))
                QueenSoul = tag.GetBool("QueenSoul");
            if (tag.ContainsKey("DestroyerSoul"))
                DestroyerSoul = tag.GetBool("DestroyerSoul");
            if (tag.ContainsKey("TwinSoul"))
                TwinSoul = tag.GetBool("TwinSoul");
            if (tag.ContainsKey("PrimeSoul"))
                PrimeSoul = tag.GetBool("PrimeSoul");
            if (tag.ContainsKey("PlantSoul"))
                PlantSoul = tag.GetBool("PlantSoul");
            if (tag.ContainsKey("GolemSoul"))
                GolemSoul = tag.GetBool("GolemSoul");
            if (tag.ContainsKey("DukeSoul"))
                DukeSoul = tag.GetBool("DukeSoul");
            if (tag.ContainsKey("EmpressSoul"))
                EmpressSoul = tag.GetBool("EmpressSoul");
            if (tag.ContainsKey("LunaticSoul"))
                LunaticSoul = tag.GetBool("LunaticSoul");
            if (tag.ContainsKey("LordSoul"))
                LordSoul = tag.GetBool("LordSoul");
            if (tag.ContainsKey("LandSoul"))
                LandSoul = tag.GetBool("LandSoul");
            if (tag.ContainsKey("TimeSoul"))
                TimeSoul = tag.GetBool("TimeSoul");
            if (tag.ContainsKey("DeathSoul"))
                DeathSoul = tag.GetBool("DeathSoul");

            if (tag.ContainsKey("selectedSoul"))
                selectedSoul = tag.GetInt("selectedSoul");
            if (tag.ContainsKey("TomeKnowledge"))
                selectedSoul = tag.GetInt("TomeKnowledge");
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return base.CanConsumeAmmo(weapon, ammo);
            }
            if (slayerSet)
                return Main.rand.NextFloat() >= .1f;
            return base.CanConsumeAmmo(weapon, ammo);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            if (ShardsOfAtheria.TomeKey.JustPressed && omnicientTome)
            {
                if (TomeKnowledge == 2)
                {
                    TomeKnowledge = 0;
                }
                else TomeKnowledge += 1;
                SoundEngine.PlaySound(SoundID.Item1, Player.position);
            }

            if ((LunaticSoul || DukeSoul) && ShardsOfAtheria.SoulTeleport.JustPressed
                && !Player.HasBuff(ModContent.BuffType<SoulTeleportCooldown>()))
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
                        Player.AddBuff(ModContent.BuffType<SoulTeleportCooldown>(), 3600);
                    }
                }
            }
        }

        public override void PostUpdateBuffs()
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            if (Player.HasBuff(ModContent.BuffType<BaseExploration>()))
            {
                Player.moveSpeed += .1f;
            }
            if (KingSoul)
            {
                Player.lifeRegen += 4;
                Player.manaRegen += 4;
            }
            if (ValkyrieSoul)
            {
                Player.statDefense += 16;
                Player.wingTimeMax += 40;

                ValkyrieDashPlayer mp = Player.GetModPlayer<ValkyrieDashPlayer>();

                if (mp.DashActive)
                {
                    if (Player.ownedProjectileCounts[ModContent.ProjectileType<ElectricTrailFriendly>()] < 20)
                        valkyrieDashTimer--;
                    if (valkyrieDashTimer == 0)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<ElectricTrailFriendly>(), 18, 0f, Player.whoAmI);
                        valkyrieDashTimer = 2;
                    }

                    //This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
                    //Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
                    //Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
                    Player.eocDash = mp.DashTimer;
                    Player.armorEffectDrawShadowEOCShield = true;

                    //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
                    if (mp.DashTimer == ValkyrieDashPlayer.MAX_DASH_TIMER)
                    {
                        Vector2 newVelocity = Player.velocity;

                        if ((mp.DashDir == ValkyrieDashPlayer.DashLeft && Player.velocity.X > -mp.DashVelocity) || (mp.DashDir == ValkyrieDashPlayer.DashRight && Player.velocity.X < mp.DashVelocity))
                        {
                            //X-velocity is set here
                            int dashDirection = mp.DashDir == ValkyrieDashPlayer.DashRight ? 1 : -1;
                            newVelocity.X = dashDirection * mp.DashVelocity;
                        }

                        Player.velocity = newVelocity;
                    }

                    //Decrement the timers
                    mp.DashTimer--;
                    mp.DashDelay--;

                    if (mp.DashDelay == 0)
                    {
                        //The dash has ended.  Reset the fields
                        mp.DashDelay = ValkyrieDashPlayer.MAX_DASH_DELAY;
                        mp.DashTimer = ValkyrieDashPlayer.MAX_DASH_TIMER;
                        mp.DashActive = false;
                    }
                }
            }
            if (SkullSoul && !PrimeSoul)
            {
                if (Player.GetModPlayer<SoAPlayer>().inCombatTimer > 0)
                {
                    spinningTimer++;
                    if (spinningTimer == 1800)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpinSkull>(), 100, 9f, Player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Roar, Player.position);
                    }
                    if (spinningTimer >= 1800)
                    {
                        Player.statDefense += Player.statDefense/2;
                        Player.GetDamage(DamageClass.Generic) += .5f;
                    }
                    if (spinningTimer == 2100)
                        spinningTimer = 0;
                }
            }
            if (DeerclopsSoul)
            {
                // Starting search distance
                float distanceFromTarget = 200;
                int count = 0;
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Player.Center);
                        bool inRange = between < distanceFromTarget;

                        if (inRange)
                        {
                            count++;
                        }
                    }
                }
                if (count > 0)
                {
                    if (count <= 3)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            Player.GetDamage(DamageClass.Generic) += .05f;
                            Player.statDefense += 5;
                        }
                    }
                    else
                    {
                        Player.GetDamage(DamageClass.Generic) += .15f;
                        Player.statDefense += 15;
                    }
                }
            }
            if (WallSoul && Player.ownedProjectileCounts[ModContent.ProjectileType<TheHungry>()] < 5)
            {
                theHungrySpawnTimer++;
                if (theHungrySpawnTimer > 300)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Normalize(Main.MouseWorld - Player.Center), ModContent.ProjectileType<TheHungry>(), 50, 3f, Player.whoAmI);
                    theHungrySpawnTimer = 0;
                }
            }
            if (QueenSoul)
            {
                Player.lifeRegen += 8;
                Player.manaRegen += 8;
            }
            if (PrimeSoul && !Player.GetModPlayer<SynergyPlayer>().mechaMayhemSynergy)
            {
                if (Player.GetModPlayer<SoAPlayer>().inCombatTimer > 0)
                {
                    spinningTimer++;
                    if (spinningTimer == 1800)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpinPrime>(), 200, 9f, Player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Roar, Player.position);
                    }
                    if (spinningTimer >= 1800)
                    {
                        Player.statDefense *= 2;
                        Player.GetDamage(DamageClass.Generic) += 1f;
                    }
                    if (spinningTimer == 2100)
                        spinningTimer = 0;
                }
            }
            if (PlantSoul && Player.ownedProjectileCounts[ModContent.ProjectileType<YourTentacle>()] < 8)
            {
                Player.moveSpeed += .15f;
                if (++yourTentacleSpawnTimer > 300)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Normalize(Main.MouseWorld - Player.Center), ModContent.ProjectileType<YourTentacle>(), 50, 3f, Player.whoAmI);
                    yourTentacleSpawnTimer = 0;
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            if (BrainSoul)
            {
                Player.noKnockback = false;
            }
            if (GolemSoul)
            {
                Player.shinyStone = true;
            }
        }

        public override void PreUpdate()
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
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
            if (spiderClock)
            {
                saveTimer++;
                if (saveTimer == 300)
                {
                    recentPos = Player.position;
                    recentLife = Player.statLife;
                    recentMana = Player.statMana;
                    CombatText.NewText(Player.getRect(), Color.Gray, "Time shift ready");
                }
                if (saveTimer >= 302)
                    saveTimer = 302;
            }
            else saveTimer = 0;
        }

        public override void PostUpdate()
        {
            if (soulCrystals >= 6 && Player.name != "Trevor Mendez" && Player.name != "Luna Mendez")
            {
                Player.AddBuff(ModContent.BuffType<Madness>(), 600);
            }
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            else
            {
                Player.statDefense -= defenseReduction;

                if (soulCrystalProjectileCooldown > 0)
                    soulCrystalProjectileCooldown--;

                if (!Player.GetModPlayer<SoAPlayer>().inCombat)
                {
                    if (++defenseRegenTime == 1 && defenseReduction > 0)
                    {
                        defenseRegenTime = 0;
                        defenseReduction--;
                    }
                }

                if (defenseReduction < 0)
                    defenseReduction = 0;
            }

            if (Player.timeSinceLastDashStarted < 5 && EyeSoul && Player.ownedProjectileCounts[ModContent.ProjectileType<Servant>()] < 3)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Player.velocity.RotatedByRandom(MathHelper.ToRadians(360)) * 1f, ModContent.ProjectileType<Servant>(), 15, 1, Player.whoAmI);
            }
            if (BrainSoul && !Player.GetModPlayer<SynergyPlayer>().brainLordSynergy)
            {
                if (!Player.HasBuff(ModContent.BuffType<CreeperShield>()))
                    creeperSpawnTimer++;
                else creeperSpawnTimer = 0;
                if (creeperSpawnTimer >= 360)
                {
                    if (Player.HasBuff(ModContent.BuffType<CreeperShield>()))
                        return;
                    else
                    {
                        NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                        NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                        NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
                        NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
                    }
                    creeperSpawnTimer = 0;
                }
            }
            if (BeeSoul)
            {
                if (Player.GetModPlayer<SoAPlayer>().inCombat)
                {
                    beeSpawnTimer++;
                    if (beeSpawnTimer >= 600)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Normalize(Main.MouseWorld - Player.Center), ProjectileID.Bee, 1, 0, Player.whoAmI);
                        beeSpawnTimer = 0;
                    }
                }
            }
            if (DestroyerSoul)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center - new Vector2(0, 90), Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0, Player.whoAmI);
                }
            }
            if (PlantSoul)
            {
                if (Player.statLife > Player.statLifeMax2 / 2)
                {
                    Player.statDefense += 15;
                }
            }
            if (GolemSoul)
            {
                if (Player.statLife < Player.statLifeMax2 / 2)
                {
                    Player.lifeRegen += 15;
                    if (Player.ownedProjectileCounts[ModContent.ProjectileType<GolemHead>()] == 0)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center - new Vector2(0, 90), Vector2.Zero, ModContent.ProjectileType<GolemHead>(), 0, 0, Player.whoAmI);
                    }
                }
            }
            if (DukeSoul)
            {
                Player.wingTimeMax += 120;
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<Sharknado>()] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center - new Vector2(0, 90), Vector2.Zero, ModContent.ProjectileType<Sharknado>(), 0, 0, Player.whoAmI);
                }
            }
            if (EmpressSoul)
            {
                Player.wingTimeMax += 120;
                Player.AddBuff(BuffID.Shine, 2);
                Player.AddBuff(BuffID.NightOwl, 2);
                if (Main.dayTime)
                {
                    Player.GetDamage(DamageClass.Generic) += .20f;
                }
                else Player.statDefense += 20;
            }
            if (LunaticSoul)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<CultistRitual>()] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<CultistRitual>(), 0, 0, Player.whoAmI);
                }
                if (lunaticCircleFragments > 5)
                    lunaticCircleFragments = 5;
            }
            else lunaticCircleFragments = 1;
            if (LordSoul)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeOfCthulhu>()] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<TrueEyeOfCthulhu>(), 0, 0, Player.whoAmI);
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            if (vampiricJaw && item.DamageType == DamageClass.Melee && !item.noMelee)
            {
                Player.HealEffect(item.damage / 5);
                Player.statLife += item.damage / 5;
            }
            if (lastDamageTaken > 0)
            {
                if (KingSoul && !QueenSoul)
                {
                    Player.statLife += lastDamageTaken/4;
                    Player.HealEffect(lastDamageTaken/4);
                }
                else if (QueenSoul)
                {
                    Player.statLife += lastDamageTaken/2;
                    Player.HealEffect(lastDamageTaken/2);
                }
                lastDamageTaken = 0;
            }
            if (BeeSoul)
            {
                target.AddBuff(BuffID.Poisoned, 600);
            }
            if (TwinSoul)
            {
                target.AddBuff(BuffID.Ichor, 600);
                target.AddBuff(BuffID.CursedInferno, 600);
            }
            if (EmpressSoul)
            {
                Vector2 position = target.Center+Vector2.One.RotatedByRandom(360)*180;
                Projectile.NewProjectile(Player.GetSource_FromThis(), position, Vector2.Normalize(target.Center - position) * 20, ProjectileID.FairyQueenRangedItemShot, 50, 6f, Player.whoAmI);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            if (lastDamageTaken > 0)
            {
                if (KingSoul && !QueenSoul)
                {
                    Player.statLife += lastDamageTaken/4;
                    Player.HealEffect(lastDamageTaken/4);
                }
                else if (QueenSoul)
                {
                    Player.statLife += lastDamageTaken/2;
                    Player.HealEffect(lastDamageTaken/2);
                }
                lastDamageTaken = 0;
            }
            if (BeeSoul)
            {
                target.AddBuff(BuffID.Poisoned, 600);
            }
            if (TwinSoul && !(target.HasBuff(BuffID.Ichor) || target.HasBuff(BuffID.CursedInferno)))
            {
                var twinDebuff = new WeightedRandom<int>();
                twinDebuff.Add(BuffID.CursedInferno);
                twinDebuff.Add(BuffID.Ichor);

                target.AddBuff(twinDebuff, 600);
            }
            if (EmpressSoul && proj.type != ProjectileID.FairyQueenRangedItemShot)
            {
                Vector2 position = target.Center+Vector2.One.RotatedByRandom(360)*180;
                Projectile.NewProjectile(Player.GetSource_FromThis(), position, Vector2.Normalize(target.Center - position) * 20, ProjectileID.FairyQueenRangedItemShot, 50, 6f, Player.whoAmI);
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            if (TwinSoul)
            {
                Vector2 position = npc.Center+Vector2.One.RotatedByRandom(360)*180;
                int getDamage = Player.GetModPlayer<SynergyPlayer>().mechaMayhemSynergy ? 60 : 40;
                Projectile.NewProjectile(Player.GetSource_OnHurt(npc), position, Vector2.Normalize(npc.Center - position) * 10, ProjectileID.FirstFractal, getDamage, 6f, Player.whoAmI);
            }
            if (DeerclopsSoul)
            {
                Vector2 position = npc.Center+Vector2.One.RotatedByRandom(360)*180;
                Projectile.NewProjectile(Player.GetSource_OnHurt(npc), position, Vector2.Normalize(npc.Center - position) * 10, ProjectileID.InsanityShadowFriendly, 26, 6f, Player.whoAmI);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
            }
            if (Player.HasBuff(ModContent.BuffType<CreeperShield>()) && damageSource.SourceProjectileType != ModContent.ProjectileType<ExtractingSoul>())
            {
                return false;
            }
            if (LunaticSoul && !Player.immune)
            {
                if (Player.whoAmI == Main.myPlayer && Main.rand.NextFloat() < 0.1f)
                {
                    Player.immuneTime = 60;
                    Player.immune = true;
                    lunaticCircleFragments++;
                    return false;
                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            totalDamageTaken += (int)damage;
            lastDamageTaken = (int)damage;

            if (ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                defenseReduction += (int)damage;
            }

            if (QueenSoul)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<CrystalExplosion>(), 60, 6f, Player.whoAmI);
            }
            if (totalDamageTaken >= 100)
            {
                if (Player.GetModPlayer<SynergyPlayer>().kingQueenSynergy && Player.ownedProjectileCounts[ModContent.ProjectileType<Slime>()] < 10)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<Slime>(), 30, 1f, Player.whoAmI);
                }
                if (DestroyerSoul && Player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbeAttack>()] < 5)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbeAttack>(), 50, 0f, Player.whoAmI);
                }
                if (LordSoul && Player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeOfCthulhuAttack>()] < 2)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<TrueEyeOfCthulhuAttack>(), 90, 2f, Player.whoAmI);
                }
                totalDamageTaken = 0;
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (damageSource.SourceProjectileType == ModContent.ProjectileType<ExtractingSoul>())
            {
                if (soulCrystals <= 1)
                {
                    if (Player.Male)
                    {
                        damageSource = PlayerDeathReason.ByCustomReason(Player.name + " had his heart ripped out.");
                    }
                    else damageSource = PlayerDeathReason.ByCustomReason(Player.name + " had her heart ripped out.");
                }
                else
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " had dug too deep.");
                }
            }
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
            }
            if (!Player.HasBuff(ModContent.BuffType<EaterReviveCooldown>()) && EaterSoul)
            {
                Player.AddBuff(ModContent.BuffType<EaterReviveCooldown>(), 18000);
                Player.statLife = Player.statLifeMax2;
                return false;
            }

            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            lunaticCircleFragments = 0;
        }

        public override void OnRespawn(Player player)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            defenseReduction = 0;
            if (EyeSoul)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
            }
            if (BrainSoul&& !Player.GetModPlayer<SynergyPlayer>().brainLordSynergy)
            {
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
            }
            if (DestroyerSoul)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0f, player.whoAmI);
            }
        }

        public override void OnEnterWorld(Player player)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return;
            }
            if (EyeSoul)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
            }
            if (BrainSoul && !Player.GetModPlayer<SynergyPlayer>().brainLordSynergy)
            {
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
            }
            if (DestroyerSoul)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0f, player.whoAmI);
            }
        }
    }

    public class ValkyrieDashPlayer : ModPlayer
    {
        //These indicate what direction is what in the timer arrays used
        public static readonly int DashRight = 2;
        public static readonly int DashLeft = 3;

        //The direction the player is currently dashing towards.  Defaults to -1 if no dash is ocurring.
        public int DashDir = -1;

        //The fields related to the dash accessory
        public bool DashActive = false;
        public int DashDelay = MAX_DASH_DELAY;
        public int DashTimer = MAX_DASH_TIMER;
        //The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
        public readonly float DashVelocity = 13f;
        //These two fields are the max values for the delay between dashes and the length of the dash in that order
        //The time is measured in frames
        public static readonly int MAX_DASH_DELAY = 50;
        public static readonly int MAX_DASH_TIMER = 20;

        public override void ResetEffects()
        {
            //ResetEffects() is called not long after player.doubleTapCardinalTimer's values have been set

            //Check if the ExampleDashAccessory is equipped and also check against this priority:
            // If the Shield of Cthulhu, Master Ninja Gear, Tabi and/or Solar Armour set is equipped, prevent this accessory from doing its dash effect
            //The priority is used to prevent undesirable effects.
            //Without it, the player is able to use the ExampleDashAccessory's dash as well as the vanilla ones
            bool dashAccessoryEquipped = false;

            //This is the loop used in vanilla to update/check the not-vanity accessories
            for (int i = 3; i < 8 + Player.extraAccessorySlots; i++)
            {
                Item item = Player.armor[i];

                //Set the flag for the ExampleDashAccessory being equipped if we have it equipped OR immediately return if any of the accessories are
                // one of the higher-priority ones
                if (Player.GetModPlayer<SlayerPlayer>().ValkyrieSoul)
                    dashAccessoryEquipped = true;
                else if (item.type == ItemID.EoCShield || item.type == ItemID.MasterNinjaGear || item.type == ItemID.Tabi)
                    return;
            }

            //If we don't have the ExampleDashAccessory equipped or the player has the Solor armor set equipped, return immediately
            //Also return if the player is currently on a mount, since dashes on a mount look weird, or if the dash was already activated
            if (!dashAccessoryEquipped || Player.setSolar || Player.mount.Active || DashActive)
                return;

            if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
                DashDir = DashRight;
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
                DashDir = DashLeft;
            else
                return;  //No dash was activated, return

            DashActive = true;

            //Here you'd be able to set an effect that happens when the dash first activates
            //Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
        }
    }
}
