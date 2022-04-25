using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Tools;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace ShardsOfAtheria
{
    public class SlayerPlayer : ModPlayer
    {
        public bool anySoulCrystals;
        public bool blueprintRead;

        public int KingSoul;
        public int EyeSoul;
        public int BrainSoul;
        public int EaterSoul;
        public int ValkyrieSoul;
        public int BeeSoul;
        public int SkullSoul;
        public int DeerclopsSoul;
        public int WallSoul;
        public int QueenSoul;
        public int DestroyerSoul;
        public int TwinSoul;
        public int PrimeSoul;
        public int PlantSoul;
        public int GolemSoul;
        public int DukeSoul;
        public int EmpressSoul;
        public int LordSoul;
        public int LandSoul;
        public int TimeSoul;
        public int DeathSoul;

        public int creeperSpawnTimer;
        public int theHungrySpawnTimer;
        public int yourTentacleSpawnTimer;
        public int valkyrieDashTimer = 2;
        public int spinningTimer;
        public int totalDamageTaken;
        public int lastDamageTaken;

        public Item[] savedInventory;
        public Item[] savedArmor;
        public Item[] savedAccessories;

        public int selectedSoul;

        public override void Initialize()
        {
            blueprintRead = false;
            KingSoul = SoulCrystalStatus.None;
            EyeSoul = SoulCrystalStatus.None;
            BrainSoul = SoulCrystalStatus.None;
            EaterSoul = SoulCrystalStatus.None;
            ValkyrieSoul = SoulCrystalStatus.None;
            BeeSoul = SoulCrystalStatus.None;
            SkullSoul = SoulCrystalStatus.None;
            WallSoul = SoulCrystalStatus.None;
            WallSoul = SoulCrystalStatus.None;
            DestroyerSoul = SoulCrystalStatus.None;
            DestroyerSoul = SoulCrystalStatus.None;
            TwinSoul = SoulCrystalStatus.None;
            PrimeSoul = SoulCrystalStatus.None;
            PlantSoul = SoulCrystalStatus.None;
            GolemSoul = SoulCrystalStatus.None;
            DukeSoul = SoulCrystalStatus.None;
            EmpressSoul = SoulCrystalStatus.None;
            LordSoul = SoulCrystalStatus.None;
            LandSoul = SoulCrystalStatus.None;
            TimeSoul = SoulCrystalStatus.None;
            DeathSoul = SoulCrystalStatus.None;

            selectedSoul = SelectedSoul.None;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["blueprintRead"] = blueprintRead;
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
            tag["LordSoul"] = LordSoul;
            tag["LandSoul"] = LandSoul;
            tag["TimeSoul"] = TimeSoul;
            tag["DeathSoul"] = DeathSoul;

            tag["selectedSoul"] = selectedSoul;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("blueprintRead"))
                blueprintRead = tag.GetBool("blueprintRead");

            if (tag.ContainsKey("KingSoul"))
                KingSoul = tag.GetInt("KingSoul");
            if (tag.ContainsKey("EyeSoul"))
                EyeSoul = tag.GetInt("EyeSoul");
            if (tag.ContainsKey("BrainSoul"))
                BrainSoul = tag.GetInt("BrainSoul");
            if (tag.ContainsKey("EaterSoul"))
                EaterSoul = tag.GetInt("EaterSoul");
            if (tag.ContainsKey("ValkyrieSoul"))
                ValkyrieSoul = tag.GetInt("ValkyrieSoul");
            if (tag.ContainsKey("BeeSoul"))
                BeeSoul = tag.GetInt("BeeSoul");
            if (tag.ContainsKey("SkullSoul"))
                SkullSoul = tag.GetInt("SkullSoul");
            if (tag.ContainsKey("DeerclopsSoul"))
                DeerclopsSoul = tag.GetInt("DeerclopsSoul");
            if (tag.ContainsKey("WallSoul"))
                WallSoul = tag.GetInt("WallSoul");
            if (tag.ContainsKey("QueenSoul"))
                QueenSoul = tag.GetInt("QueenSoul");
            if (tag.ContainsKey("DestroyerSoul"))
                DestroyerSoul = tag.GetInt("DestroyerSoul");
            if (tag.ContainsKey("TwinSoul"))
                TwinSoul = tag.GetInt("TwinSoul");
            if (tag.ContainsKey("PrimeSoul"))
                PrimeSoul = tag.GetInt("PrimeSoul");
            if (tag.ContainsKey("PlantSoul"))
                PlantSoul = tag.GetInt("PlantSoul");
            if (tag.ContainsKey("GolemSoul"))
                GolemSoul = tag.GetInt("GolemSoul");
            if (tag.ContainsKey("DukeSoul"))
                DukeSoul = tag.GetInt("DukeSoul");
            if (tag.ContainsKey("EmpressSoul"))
                EmpressSoul = tag.GetInt("EmpressSoul");
            if (tag.ContainsKey("LordSoul"))
                LordSoul = tag.GetInt("LordSoul");
            if (tag.ContainsKey("LandSoul"))
                LandSoul = tag.GetInt("LandSoul");
            if (tag.ContainsKey("TimeSoul"))
                TimeSoul = tag.GetInt("TimeSoul");
            if (tag.ContainsKey("DeathSoul"))
                DeathSoul = tag.GetInt("DeathSoul");

            if (tag.ContainsKey("selectedSoul"))
                selectedSoul = tag.GetInt("selectedSoul");
        }

        public override void PostUpdateBuffs()
        {
            if (KingSoul == SoulCrystalStatus.Absorbed)
            {
                Player.lifeRegen += 4;
                Player.manaRegen += 4;
            }
            if (ValkyrieSoul == SoulCrystalStatus.Absorbed)
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
                        Projectile.NewProjectile(Player.GetProjectileSource_Misc(ModContent.ItemType<ValkyrieSoulCrystal>()), Player.Center, Vector2.Zero, ModContent.ProjectileType<ElectricTrailFriendly>(), 18, 0f, Player.whoAmI);
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
            if (BeeSoul == SoulCrystalStatus.Absorbed)
            {
                Player.moveSpeed += .05f;
                Player.strongBees = true;
            }
            if (SkullSoul == SoulCrystalStatus.Absorbed && PrimeSoul == SoulCrystalStatus.None)
            {
                if (Player.GetModPlayer<SoAPlayer>().inCombat)
                {
                    spinningTimer++;
                    if (spinningTimer == 1800)
                    {
                        Projectile.NewProjectile(Player.GetProjectileSource_Misc(ModContent.ItemType<SkullSoulCrystal>()), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpinSkull>(), 100, 9f, Player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Roar, Player.position, 0);
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
            if (WallSoul == SoulCrystalStatus.Absorbed && Player.ownedProjectileCounts[ModContent.ProjectileType<TheHungry>()] < 5)
            {
                theHungrySpawnTimer++;
                if (theHungrySpawnTimer > 300)
                {
                    Projectile.NewProjectile(Player.GetProjectileSource_Misc(ModContent.ItemType<WallSoulCrystal>()), Player.Center, Vector2.Normalize(Main.MouseWorld - Player.Center), ModContent.ProjectileType<TheHungry>(), 50, 3f, Player.whoAmI);
                    theHungrySpawnTimer = 0;
                }
            }
            if (PrimeSoul == SoulCrystalStatus.Absorbed)
            {
                if (Player.GetModPlayer<SoAPlayer>().inCombat)
                {
                    spinningTimer++;
                    if (spinningTimer == 1800)
                    {
                        Projectile.NewProjectile(Player.GetProjectileSource_Misc(ModContent.ItemType<PrimeSoulCrystal>()), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpinPrime>(), 200, 9f, Player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Roar, Player.position, 0);
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
            if (PlantSoul == SoulCrystalStatus.Absorbed && Player.ownedProjectileCounts[ModContent.ProjectileType<YourTentacle>()] < 8)
            {
                Player.moveSpeed += .15f;
                if (++yourTentacleSpawnTimer > 300)
                {
                    Projectile.NewProjectile(Player.GetProjectileSource_Misc(ModContent.ItemType<WallSoulCrystal>()), Player.Center, Vector2.Normalize(Main.MouseWorld - Player.Center), ModContent.ProjectileType<YourTentacle>(), 50, 3f, Player.whoAmI);
                    yourTentacleSpawnTimer = 0;
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (BrainSoul == SoulCrystalStatus.Absorbed)
            {
                Player.noKnockback = false;
            }
        }

        public override void PostUpdate()
        {
            if (ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                if (!Player.HasBuff(ModContent.BuffType<ImmunityBuff>()))
                    Player.immuneTime = 0;
                Player.statDefense /= 2;
                Player.endurance /= 2;
            }

            if (Player.timeSinceLastDashStarted < 5 && EyeSoul == SoulCrystalStatus.Absorbed && Player.ownedProjectileCounts[ModContent.ProjectileType<Servant>()] < 3)
            {
                Projectile.NewProjectile(Player.GetProjectileSource_Misc(EyeSoul), Player.Center, Player.velocity.RotatedByRandom(MathHelper.ToRadians(360)) * 1f, ModContent.ProjectileType<Servant>(), 15, 1, Player.whoAmI);
            }
            if (BrainSoul == SoulCrystalStatus.Absorbed)
            {
                if (!Player.HasBuff(ModContent.BuffType<CreeperShield>()))
                    creeperSpawnTimer++;
                if (creeperSpawnTimer >= 18000)
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

            if (KingSoul == SoulCrystalStatus.Absorbed || EyeSoul == SoulCrystalStatus.Absorbed || BrainSoul == SoulCrystalStatus.Absorbed || EaterSoul == SoulCrystalStatus.Absorbed
                || ValkyrieSoul == SoulCrystalStatus.Absorbed || BeeSoul == SoulCrystalStatus.Absorbed || SkullSoul == SoulCrystalStatus.Absorbed || WallSoul == SoulCrystalStatus.Absorbed
                || DestroyerSoul == SoulCrystalStatus.Absorbed || TwinSoul == SoulCrystalStatus.Absorbed || PrimeSoul == SoulCrystalStatus.Absorbed || PlantSoul == SoulCrystalStatus.Absorbed
                || DukeSoul == SoulCrystalStatus.Absorbed || EmpressSoul == SoulCrystalStatus.Absorbed || LordSoul == SoulCrystalStatus.Absorbed || LandSoul == SoulCrystalStatus.Absorbed
                || TimeSoul == SoulCrystalStatus.Absorbed || DeathSoul == SoulCrystalStatus.Absorbed)
                anySoulCrystals = true;
            else anySoulCrystals = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (lastDamageTaken > 0)
            {
                if (KingSoul == SoulCrystalStatus.Absorbed && QueenSoul == SoulCrystalStatus.None)
                {
                    Player.statLife += lastDamageTaken/4;
                    Player.HealEffect(lastDamageTaken/4);
                }
                else if (QueenSoul == SoulCrystalStatus.Absorbed)
                {
                    Player.statLife += lastDamageTaken/2;
                    Player.HealEffect(lastDamageTaken/2);
                }
                lastDamageTaken = 0;
            }
            if (TwinSoul == SoulCrystalStatus.Absorbed)
            {
                target.AddBuff(BuffID.Ichor, 600);
                target.AddBuff(BuffID.CursedInferno, 600);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (lastDamageTaken > 0)
            {
                if (KingSoul == SoulCrystalStatus.Absorbed && QueenSoul == SoulCrystalStatus.None)
                {
                    Player.statLife += lastDamageTaken/4;
                    Player.HealEffect(lastDamageTaken/4);
                }
                else if (QueenSoul == SoulCrystalStatus.Absorbed)
                {
                    Player.statLife += lastDamageTaken/2;
                    Player.HealEffect(lastDamageTaken/2);
                }
                lastDamageTaken = 0;
            }
            if (TwinSoul == SoulCrystalStatus.Absorbed && !(target.HasBuff(BuffID.Ichor) || target.HasBuff(BuffID.CursedInferno)))
            {
                var twinDebuff = new WeightedRandom<int>();
                twinDebuff.Add(BuffID.CursedInferno);
                twinDebuff.Add(BuffID.Ichor);

                target.AddBuff(twinDebuff, 600);
            }
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (Player.HasBuff(ModContent.BuffType<CreeperShield>()))
                return false;
            return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (Player.HasBuff(ModContent.BuffType<CreeperShield>()))
                return false;
            return base.CanBeHitByProjectile(proj);
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (TwinSoul == SoulCrystalStatus.Absorbed)
            {
                Vector2 position = npc.Center+Vector2.One.RotatedByRandom(360)*180;
                Projectile.NewProjectile(Player.GetProjectileSource_OnHurt(npc, 0), position, Vector2.Normalize(npc.Center - position) * 10, ProjectileID.FirstFractal, damage, 6f, Player.whoAmI);
            }
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (BeeSoul == SoulCrystalStatus.Absorbed && !Player.immune)
            {
                int num12 = 1;
                if (Main.rand.Next(3) == 0)
                {
                    num12++;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num12++;
                }
                if (Player.strongBees && Main.rand.Next(3) == 0)
                {
                    num12++;
                }
                float num13 = 13f;
                if (Player.strongBees)
                {
                    num13 = 18f;
                }
                if (Main.masterMode)
                {
                    num13 *= 2f;
                }
                else if (Main.expertMode)
                {
                    num13 *= 1.5f;
                }
                for (int num14 = 0; num14 < num12; num14++)
                {
                    float speedX = (float)Main.rand.Next(-35, 36) * 0.02f;
                    float speedY = (float)Main.rand.Next(-35, 36) * 0.02f;
                    Projectile.NewProjectile(Player.GetProjectileSource_Accessory(Player.honeyCombItem), Player.position.X, Player.position.Y, speedX, speedY, Player.beeType(), Player.beeDamage((int)num13),
                        Player.beeKB(0f), Main.myPlayer);
                }
                Player.AddBuff(BuffID.Honey, 300);
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            totalDamageTaken += (int)damage;
            lastDamageTaken = (int)damage;
            if (DestroyerSoul == SoulCrystalStatus.Absorbed)
            {
                if (totalDamageTaken >= 100)
                {
                    Projectile.NewProjectile(Player.GetProjectileSource_Misc(ModContent.ItemType<DestroyerSoulCrystal>()), Player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbeAttack>(), 50, 0f, Player.whoAmI);
                    totalDamageTaken = 0;
                }
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            savedInventory = (Item[])Player.inventory.Clone();
            savedArmor = (Item[])Player.armor.Clone();
            savedAccessories = (Item[])Player.miscEquips.Clone();
            if (!Player.HasBuff(ModContent.BuffType<EaterReviveCooldown>()) && EaterSoul == SoulCrystalStatus.Absorbed)
            {
                Player.AddBuff(ModContent.BuffType<EaterReviveCooldown>(), 18000);
                Player.AddBuff(ModContent.BuffType<ImmunityBuff>(), 60);
                Player.statLife = Player.statLifeMax2;
                return false;
            }

            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (KingSoul == SoulCrystalStatus.Absorbed && QueenSoul == SoulCrystalStatus.None)
            {
                int npcIndex = NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)Player.Center.X, (int)Player.Center.Y, ModContent.NPCType<CarrierSlime>());
                (Main.npc[npcIndex].ModNPC as CarrierSlime).inventoryItems = (Item[])savedInventory.Clone();
                (Main.npc[npcIndex].ModNPC as CarrierSlime).armor = (Item[])savedArmor.Clone();
                (Main.npc[npcIndex].ModNPC as CarrierSlime).accessories = (Item[])savedAccessories.Clone();
                (Main.npc[npcIndex].ModNPC as CarrierSlime).coins = Player.lostCoins;
            }
        }

        public override void OnRespawn(Player player)
        {
            if (EyeSoul == SoulCrystalStatus.Absorbed)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Misc(ModContent.ItemType<EyeSoulCrystal>()), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
            }
            if (BrainSoul == SoulCrystalStatus.Absorbed)
            {
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
            }
            if (DestroyerSoul == SoulCrystalStatus.Absorbed)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                    Projectile.NewProjectile(player.GetProjectileSource_Misc(ModContent.ItemType<DestroyerSoulCrystal>()), player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0f, player.whoAmI);
            }
        }

        public override void OnEnterWorld(Player player)
        {
            if (EyeSoul == SoulCrystalStatus.Absorbed)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Misc(ModContent.ItemType<EyeSoulCrystal>()), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
            }
            if (BrainSoul == SoulCrystalStatus.Absorbed)
            {
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y + 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X + 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
                NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)(Player.Center.X - 20), (int)(Player.Center.Y - 20), ModContent.NPCType<Creeper>());
            }
            if (DestroyerSoul == SoulCrystalStatus.Absorbed)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                    Projectile.NewProjectile(player.GetProjectileSource_Misc(ModContent.ItemType<DestroyerSoulCrystal>()), player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0f, player.whoAmI);
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
                if (Player.GetModPlayer<SlayerPlayer>().ValkyrieSoul == SoulCrystalStatus.Absorbed)
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
