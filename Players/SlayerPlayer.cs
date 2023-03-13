using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.PlayerDebuff;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Tools.Misc;
using ShardsOfAtheria.NPCs.Misc;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Tools;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace ShardsOfAtheria.Players
{
    public class SlayerPlayer : ModPlayer
    {
        public bool slayerMode;

        public bool slayerSet;

        public List<int> soulCrystals = new();

        public bool creeperPet;

        //Spider Clock
        public Vector2 recentPos;
        public int recentLife;
        public int recentMana;
        public int recentCharge;

        public int TomeKnowledge;
        public bool omnicientTome;
        public int creeperSpawnTimer;
        public int valkyrieDashTimer = 2;
        public int beeSpawnTimer;
        public int spinningTimer;
        public int theHungrySpawnTimer;
        public int yourTentacleSpawnTimer;
        public int servantSpawnTimer;

        public int soulTeleports;
        public int lunaticCircleFragments = 1;

        public int totalDamageTaken;
        public int lastDamageTaken;

        public int soulCrystalProjectileCooldown;

        public override void ResetEffects()
        {
            creeperPet = false;
        }

        public override void Initialize()
        {
            slayerMode = false;

            slayerSet = false;

            TomeKnowledge = 0;
            omnicientTome = false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["slayerMode"] = slayerMode;
            tag["soulCrystals"] = soulCrystals;

            tag.Add("soulCrystalsList", soulCrystals);
            tag["TomeKnowledge"] = TomeKnowledge;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("slayerMode"))
            {
                slayerMode = tag.GetBool("slayerMode");
            }
            if (tag.ContainsKey("soulCrystalsList"))
            {
                soulCrystals = tag.Get<List<int>>("soulCrystalsList");
            }
            if (tag.ContainsKey("TomeKnowledge"))
            {
                TomeKnowledge = tag.GetInt("TomeKnowledge");
            }
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)ShardsOfAtheriaMod.MessageType.SyncSoulCrystals);
            packet.Write((byte)Player.whoAmI);
            for (int i = 0; i < soulCrystals.Count; i++)
            {
                packet.Write(soulCrystals[i]);
            }
            packet.Send(toWho, fromWho);
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath)
            {
                return new[] { new Item(ModContent.ItemType<Necronomicon>()) };
            }

            return base.AddStartingItems(mediumCoreDeath);
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (slayerSet)
            {
                return Main.rand.NextFloat() >= .1f;
            }
            return base.CanConsumeAmmo(weapon, ammo);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ShardsOfAtheriaMod.TomeKey.JustPressed && omnicientTome)
            {
                if (TomeKnowledge == 2)
                {
                    TomeKnowledge = 0;
                }
                else TomeKnowledge += 1;
                SoundEngine.PlaySound(SoundID.Item1, Player.position);
            }

            if ((soulCrystals.Contains(ModContent.ItemType<LunaticSoulCrystal>()) || soulCrystals.Contains(ModContent.ItemType<DukeSoulCrystal>())) && ShardsOfAtheriaMod.SoulTeleport.JustPressed
                && !Player.HasBuff(ModContent.BuffType<SoulTeleportCooldown>()))
            {
                Vector2 vector21 = default;
                vector21.X = Main.mouseX + Main.screenPosition.X;
                if (Player.gravDir == 1f)
                {
                    vector21.Y = Main.mouseY + Main.screenPosition.Y - Player.height;
                }
                else
                {
                    vector21.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
                }
                vector21.X -= Player.width / 2;
                if (vector21.X > 50f && vector21.X < Main.maxTilesX * 16 - 50 && vector21.Y > 50f && vector21.Y < Main.maxTilesY * 16 - 50)
                {
                    int num181 = (int)(vector21.X / 16f);
                    int num182 = (int)(vector21.Y / 16f);
                    if ((Main.tile[num181, num182].WallType != 87 || !(num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, Player.width, Player.height))
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
            if (Player.HasBuff(ModContent.BuffType<BaseExploration>()))
            {
                Player.moveSpeed += .1f;
            }
            if (soulCrystals.Contains(ModContent.ItemType<KingSoulCrystal>()))
            {
                Player.lifeRegen += 4;
                Player.manaRegen += 4;
            }
            if (soulCrystals.Contains(ModContent.ItemType<ValkyrieSoulCrystal>()))
            {
                Player.statDefense += 8;
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

                        if (mp.DashDir == ValkyrieDashPlayer.DashLeft && Player.velocity.X > -mp.DashVelocity || mp.DashDir == ValkyrieDashPlayer.DashRight && Player.velocity.X < mp.DashVelocity)
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
            if (soulCrystals.Contains(ModContent.ItemType<SkullSoulCrystal>()) && !soulCrystals.Contains(ModContent.ItemType<PrimeSoulCrystal>()))
            {
                if (Player.ShardsOfAtheria().inCombat)
                {
                    spinningTimer++;
                    if (spinningTimer == 1800)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpinSkull>(), 100, 9f, Player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Roar, Player.position);
                    }
                    if (spinningTimer >= 1800)
                    {
                        Player.statDefense += Player.statDefense / 2;
                        Player.GetDamage(DamageClass.Generic) += .5f;
                    }
                    if (spinningTimer == 2100)
                        spinningTimer = 0;
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<DeerclopsSoulCrystal>()))
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
            if (soulCrystals.Contains(ModContent.ItemType<WallSoulCrystal>()) && Player.ownedProjectileCounts[ModContent.ProjectileType<TheHungry>()] < 5)
            {
                theHungrySpawnTimer++;
                if (theHungrySpawnTimer > 300)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Normalize(Main.MouseWorld - Player.Center), ModContent.ProjectileType<TheHungry>(), 50, 3f, Player.whoAmI);
                    theHungrySpawnTimer = 0;
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<QueenSoulCrystal>()))
            {
                Player.lifeRegen += 8;
                Player.manaRegen += 8;
            }
            if (soulCrystals.Contains(ModContent.ItemType<PrimeSoulCrystal>()))
            {
                if (Player.ShardsOfAtheria().inCombat)
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
            if (soulCrystals.Contains(ModContent.ItemType<PlantSoulCrystal>()) && Player.ownedProjectileCounts[ModContent.ProjectileType<YourTentacle>()] < 8)
            {
                Player.moveSpeed += .15f;
                if (++yourTentacleSpawnTimer > 300)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Normalize(Main.MouseWorld - Player.Center), ModContent.ProjectileType<YourTentacle>(), 50, 3f, Player.whoAmI);
                    yourTentacleSpawnTimer = 0;
                }
            }
        }

        public override void UpdateEquips()
        {
            if (soulCrystals.Contains(ModContent.ItemType<BrainSoulCrystal>()))
            {
                Player.noKnockback = false;
            }
            if (soulCrystals.Contains(ModContent.ItemType<GolemSoulCrystal>()))
            {
                Player.shinyStone = true;
            }
        }

        public override void PreUpdate()
        {
            if (slayerMode || soulCrystals.Count() > 0)
            {

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
        }

        public override void PostUpdate()
        {
            if (soulCrystals.Count > 6)
            {
                Player.AddBuff(ModContent.BuffType<Madness>(), 600);
            }

            if (soulCrystalProjectileCooldown > 0)
            {
                soulCrystalProjectileCooldown--;
            }

            if (soulCrystals.Contains(ModContent.ItemType<EyeSoulCrystal>()) && Player.ownedProjectileCounts[ModContent.ProjectileType<Servant>()] < 3)
            {
                if (++servantSpawnTimer >= 300)
                {
                    servantSpawnTimer = 0;
                    for (int i = 0; i < 3; ++i)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Player.velocity.RotatedByRandom(MathHelper.TwoPi) * 1f, ModContent.ProjectileType<Servant>(), 15, 1, Player.whoAmI);
                    }
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<BrainSoulCrystal>()))
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
            if (soulCrystals.Contains(ModContent.ItemType<BeeSoulCrystal>()))
            {
                if (Player.ShardsOfAtheria().inCombat)
                {
                    beeSpawnTimer++;
                    if (beeSpawnTimer >= 600)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Normalize(Main.MouseWorld - Player.Center), ProjectileID.Bee, 1, 0, Player.whoAmI);
                        beeSpawnTimer = 0;
                    }
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<DestroyerSoulCrystal>()))
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center - new Vector2(0, 90), Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0, Player.whoAmI);
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<PlantSoulCrystal>()))
            {
                if (Player.statLife > Player.statLifeMax2 / 2)
                {
                    Player.statDefense += 15;
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<GolemSoulCrystal>()))
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
            if (soulCrystals.Contains(ModContent.ItemType<DukeSoulCrystal>()))
            {
                Player.wingTimeMax += 120;
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<Sharknado>()] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center - new Vector2(0, 90), Vector2.Zero, ModContent.ProjectileType<Sharknado>(), 0, 0, Player.whoAmI);
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<EmpressSoulCrystal>()))
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
            if (soulCrystals.Contains(ModContent.ItemType<LunaticSoulCrystal>()))
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<CultistRitual>()] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<CultistRitual>(), 0, 0, Player.whoAmI);
                }
                if (lunaticCircleFragments > 5)
                    lunaticCircleFragments = 5;
            }
            else lunaticCircleFragments = 1;
            if (soulCrystals.Contains(ModContent.ItemType<LordSoulCrystal>()))
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeOfCthulhu>()] == 0)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<TrueEyeOfCthulhu>(), 0, 0, Player.whoAmI);
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (lastDamageTaken > 0)
            {
                if (soulCrystals.Contains(ModContent.ItemType<KingSoulCrystal>()) && !soulCrystals.Contains(ModContent.ItemType<QueenSoulCrystal>()))
                {
                    Player.statLife += lastDamageTaken / 4;
                    Player.Heal(lastDamageTaken / 4);
                }
                else if (soulCrystals.Contains(ModContent.ItemType<QueenSoulCrystal>()))
                {
                    Player.statLife += lastDamageTaken / 2;
                    Player.Heal(lastDamageTaken / 2);
                }
                lastDamageTaken = 0;
            }
            if (soulCrystals.Contains(ModContent.ItemType<BeeSoulCrystal>()))
            {
                target.AddBuff(BuffID.Poisoned, 600);
            }
            if (soulCrystals.Contains(ModContent.ItemType<TwinsSoulCrystal>()))
            {
                target.AddBuff(BuffID.Ichor, 600);
                target.AddBuff(BuffID.CursedInferno, 600);
            }
            if (soulCrystals.Contains(ModContent.ItemType<EmpressSoulCrystal>()))
            {
                Vector2 position = target.Center + Vector2.One.RotatedByRandom(360) * 180;
                Projectile.NewProjectile(Player.GetSource_FromThis(), position, Vector2.Normalize(target.Center - position) * 20, ProjectileID.FairyQueenRangedItemShot, 50, 6f, Player.whoAmI);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (lastDamageTaken > 0)
            {
                if (soulCrystals.Contains(ModContent.ItemType<KingSoulCrystal>()) && !soulCrystals.Contains(ModContent.ItemType<QueenSoulCrystal>()))
                {
                    Player.Heal(lastDamageTaken / 4);
                }
                else if (soulCrystals.Contains(ModContent.ItemType<QueenSoulCrystal>()))
                {
                    Player.Heal(lastDamageTaken / 2);
                }
                lastDamageTaken = 0;
            }
            if (soulCrystals.Contains(ModContent.ItemType<BeeSoulCrystal>()))
            {
                target.AddBuff(BuffID.Poisoned, 600);
            }
            if (soulCrystals.Contains(ModContent.ItemType<TwinsSoulCrystal>()) && !(target.HasBuff(BuffID.Ichor) || target.HasBuff(BuffID.CursedInferno)))
            {
                var twinDebuff = new WeightedRandom<int>();
                twinDebuff.Add(BuffID.CursedInferno);
                twinDebuff.Add(BuffID.Ichor);

                target.AddBuff(twinDebuff, 600);
            }
            if (soulCrystals.Contains(ModContent.ItemType<EmpressSoulCrystal>()) && proj.type != ProjectileID.FairyQueenRangedItemShot)
            {
                Vector2 position = target.Center + Vector2.One.RotatedByRandom(360) * 180;
                Projectile.NewProjectile(Player.GetSource_FromThis(), position, Vector2.Normalize(target.Center - position) * 20, ProjectileID.FairyQueenRangedItemShot, 50, 6f, Player.whoAmI);
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (soulCrystals.Contains(ModContent.ItemType<TwinsSoulCrystal>()))
            {
                Vector2 position = npc.Center + Vector2.One.RotatedByRandom(360) * 180;
                int getDamage = 40;
                Projectile.NewProjectile(Player.GetSource_OnHurt(npc), position, Vector2.Normalize(npc.Center - position) * 10, ProjectileID.FirstFractal, getDamage, 6f, Player.whoAmI);
            }
            if (soulCrystals.Contains(ModContent.ItemType<DeerclopsSoulCrystal>()))
            {
                Vector2 position = npc.Center + Vector2.One.RotatedByRandom(360) * 180;
                Projectile.NewProjectile(Player.GetSource_OnHurt(npc), position, Vector2.Normalize(npc.Center - position) * 10, ProjectileID.InsanityShadowFriendly, 26, 6f, Player.whoAmI);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (Player.HasBuff(ModContent.BuffType<CreeperShield>()) && damageSource.SourceProjectileType != ModContent.ProjectileType<ExtractingSoul>())
            {
                return false;
            }
            if (soulCrystals.Contains(ModContent.ItemType<LunaticSoulCrystal>()) && !Player.immune)
            {
                if (Player.whoAmI == Main.myPlayer && Main.rand.NextFloat() < 0.1f)
                {
                    Player.immuneTime = 60;
                    Player.immune = true;
                    lunaticCircleFragments++;
                    return false;
                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (slayerMode)
            {
                totalDamageTaken += (int)damage;
                lastDamageTaken = (int)damage;
            }

            if (soulCrystals.Contains(ModContent.ItemType<QueenSoulCrystal>()))
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<CrystalExplosion>(), 60, 6f, Player.whoAmI);
            }
            if (totalDamageTaken >= 100)
            {
                if (soulCrystals.Contains(ModContent.ItemType<DestroyerSoulCrystal>()) && Player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbeAttack>()] < 5)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbeAttack>(), 50, 0f, Player.whoAmI);
                }
                if (soulCrystals.Contains(ModContent.ItemType<LordSoulCrystal>()) && Player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeOfCthulhuAttack>()] < 2)
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
                if (soulCrystals.Count <= 1)
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
            if (!Player.HasBuff(ModContent.BuffType<EaterReviveCooldown>()) && soulCrystals.Contains(ModContent.ItemType<EaterSoulCrystal>()))
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
            if (soulCrystals.Contains(ModContent.ItemType<EyeSoulCrystal>()))
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
            }
            if (soulCrystals.Contains(ModContent.ItemType<BrainSoulCrystal>()))
            {
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
            }
            if (soulCrystals.Contains(ModContent.ItemType<DestroyerSoulCrystal>()))
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0f, player.whoAmI);
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<LordSoulCrystal>()))
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeOfCthulhu>()] == 0)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<TrueEyeOfCthulhu>(), 0, 0f, player.whoAmI);
                }
            }
        }

        public override void OnEnterWorld(Player player)
        {
            if (!Entry.entriesLoaded)
            {
                Entry.IncludedEntries();
                Entry.entriesLoaded = true;
            }
            if (soulCrystals.Contains(ModContent.ItemType<EyeSoulCrystal>()))
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
            }
            if (soulCrystals.Contains(ModContent.ItemType<BrainSoulCrystal>()))
            {
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
            }
            if (soulCrystals.Contains(ModContent.ItemType<DestroyerSoulCrystal>()))
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0f, player.whoAmI);
                }
            }
            if (soulCrystals.Contains(ModContent.ItemType<LordSoulCrystal>()))
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeOfCthulhu>()] == 0)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<TrueEyeOfCthulhu>(), 0, 0f, player.whoAmI);
                }
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
                if (Player.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(ModContent.ItemType<ValkyrieSoulCrystal>()))
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
            Player.timeSinceLastDashStarted = 0;

            //Here you'd be able to set an effect that happens when the dash first activates
            //Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
        }
    }
}
