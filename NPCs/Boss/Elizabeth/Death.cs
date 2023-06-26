﻿using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.GrabBags;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.PetItems;
using ShardsOfAtheria.Items.Placeable.Furniture.Trophies;
using ShardsOfAtheria.Items.Placeable.Furniture.Trophies.Master;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon.Minion;
using ShardsOfAtheria.ModCondition.ItemDrop;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.NPCProj.Elizabeth;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.Boss.Elizabeth
{
    [AutoloadBossHead]
    public class Death : ModNPC
    {
        int attackType = -1;
        int attackTimer = 0;
        int attackCooldown = 40;
        int attackTypeNext = -1;

        int frameX = 0;
        int frameY = 0;

        Color TextColor = Color.Red;
        public bool AttacksEnabled => NPC.life < NPC.lifeMax;

        public override string BossHeadTexture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            //Main.npcFrameCount[NPC.type] = 6;

            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPC.AddAqua();
            NPC.AddWood();
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 42;
            NPC.damage = 90;
            NPC.defense = 180;
            NPC.lifeMax = 300000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.boss = true;
            NPC.noGravity = true;
            Music = MusicID.Boss4;
            NPC.value = 167900;
            NPC.npcSlots = 15f;
            NPC.ElementMultipliers() = new[] { 1.0f, 0.8f, 2.0f, 0.8f };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.ShardsOfAtheria.NPCs.Death.Bestuary"))
            });
        }

        public override bool CheckDead()
        {
            KillProjectiles();
            attackType = 0;
            attackTimer = 0;
            attackCooldown = 0;
            frameX = 1;
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = 1;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            // Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            // This requires you to set BossBag in SetDefaults accordingly
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<NovaBossBag>()));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            LeadingConditionRule slayerMode = new(new IsSlayerMode());
            LeadingConditionRule master = new(new Conditions.IsMasterMode());

            int[] drops = { ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<DownBow>(), ModContent.ItemType<PlumeCodex>(), ModContent.ItemType<NestlingStaff>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, drops));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HardlightKnife>(), 5, 150, 180));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieCrown>(), 5));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HardlightPrism>(), 1, 15, 28));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.GoldBar, 1, 8, 14));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<NovaRelic>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NovaTrophy>(), 10));
            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<SmallHardlightCrest>(), 4));

            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieSoulCrystal>()));

            if (ModLoader.TryGetMod("MagicStorage", out Mod magicStorage) && !ShardsDownedSystem.downedValkyrie)
            {
                notExpertRule.OnSuccess(ItemDropRule.Common(magicStorage.Find<ModItem>("ShadowDiamond").Type));
            }

            master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieStormLance>()));
            npcLoot.Add(master);

            // Finally add the leading rule
            npcLoot.Add(notExpertRule);
            npcLoot.Add(slayerMode);
        }

        public override void OnKill()
        {
            Player lastPlayerToHitThisNPC = NPC.AnyInteractions() ? Main.player[NPC.lastInteraction] : null;
            NPC.SetEventFlagCleared(ref ShardsDownedSystem.downedDeath, -1);

            if (lastPlayerToHitThisNPC != null)
            {
                if (lastPlayerToHitThisNPC.IsSlayer())
                {
                    SoA.DownedSystem.slainDeath = true;
                    NPC.SlayNPC(lastPlayerToHitThisNPC);
                }
            }
        }

        public override bool PreAI()
        {
            if (SoA.DownedSystem.slainDeath)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Elizabeth Norman was slain..."), Color.Red);
                NPC.active = false;
            }
            return base.PreAI();
        }

        private bool phase2 = false;
        private bool desperation = false;
        bool AlreadyDefeated = ShardsDownedSystem.downedDeath;

        public override void AI()
        {
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }

            Player player = Main.player[NPC.target];
            bool isSlayer = player.GetModPlayer<SlayerPlayer>().slayerMode;

            // death drama
            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f; // increase our death timer.
                NPC.velocity = isSlayer ? new Vector2(0, 1) * 8f : Vector2.Zero;
                if (NPC.ai[3] == 2)
                {
                    NPC.UseBossDialogueWithKey("Death", isSlayer ? ShardsNPCHelper.LastWords :
                        AlreadyDefeated ? ShardsNPCHelper.ReDefeatLine :
                        ShardsNPCHelper.DefeatLine, TextColor);
                }
                if (NPC.ai[3] >= 120f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                }
                return;
            }

            if (player.dead)
            {
                attackCooldown = 0;
                attackTimer = 0;
                attackType = 0;
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }

            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !SoA.DownedSystem.slainDeath)
            {
                if (Main.rand.NextFloat() <= .5f)
                    NPC.position = player.position - new Vector2(500, 250);
                else NPC.position = player.position - new Vector2(-500, 250);
                NPC.UseBossDialogueWithKey("Death", isSlayer ? ShardsNPCHelper.SlayerSummonLine :
                    AlreadyDefeated ? ShardsNPCHelper.ReSummonLine :
                    ShardsNPCHelper.SummonLine, TextColor);
                NPC.localAI[0] = 1f;
            }
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;

            bool transitioning = false;
            if (NPC.life <= NPC.lifeMax * 0.8 && !phase2)
            {
                transitioning = true;
                attackCooldown = 120;
                attackTimer = 0;
                attackType = -1;
                NPC.UseBossDialogueWithKey("Death", isSlayer ? ShardsNPCHelper.SlayerMidFightLine :
                    AlreadyDefeated ? ShardsNPCHelper.ReMidFightLine :
                    ShardsNPCHelper.MidFightLine, TextColor);
                phase2 = true;
            }

            if (NPC.life <= NPC.lifeMax * 0.25 && !desperation && player.IsSlayer())
            {
                NPC.UseBossDialogueWithKey("Death", ShardsNPCHelper.DesperationLine, TextColor);
                desperation = true;
            }

            if (!transitioning)
            {
                if (attackCooldown <= 0)
                {
                    ChoseAttacks();
                }

                if (attackTimer > 0)
                {
                    CycleAttack(player);
                    attackTimer--;
                }
                else
                {
                    //if (phase2)
                    //{
                    //    frameX = 1;
                    //}
                    //else
                    //{
                    //    frameX = 0;
                    //}
                    // decrease wait timer when attack done
                    attackCooldown--;
                }
            }
            NPC.netUpdate = true;
        }

        int damage = 0;
        const int Crossbow = 0;
        const int BloodJavelin = 1;
        const int BloodSpike = 2;
        const int BloodOrb = 3;
        const int BloodScepter = 4;
        const int BloodSickle = 5;
        const int BloodScythe = 6;
        const int BloodSword = 7;
        List<int> blacklistedAttacks = new();
        void ChoseAttacks()
        {
            WeightedRandom<int> random = new();
            AddNonBlacklistedAttack(ref random, Crossbow);
            if (AttacksEnabled)
            {
                if (NPC.life <= NPC.lifeMax / 8 * 7)
                {
                    AddNonBlacklistedAttack(ref random, BloodJavelin);
                }
                if (NPC.life <= NPC.lifeMax / 4 * 3)
                {
                    AddNonBlacklistedAttack(ref random, BloodOrb);
                    AddNonBlacklistedAttack(ref random, BloodScepter);
                }
                if (NPC.life <= NPC.lifeMax / 2)
                {
                    AddNonBlacklistedAttack(ref random, BloodSickle);
                    AddNonBlacklistedAttack(ref random, BloodScythe);
                }
                if (Main.expertMode)
                {
                    AddNonBlacklistedAttack(ref random, BloodSpike);
                }
                if (Main.masterMode)
                {
                    AddNonBlacklistedAttack(ref random, BloodSword);
                }
            }
            attackType = random;
            if (attackTypeNext > -1)
            {
                attackType = attackTypeNext;
            }
            blacklistedAttacks.Clear();
            SetAttackStats();
        }
        void AddNonBlacklistedAttack(ref WeightedRandom<int> randomAttack, int attack, double weight = 1)
        {
            if (!blacklistedAttacks.Contains(attack))
            {
                randomAttack.Add(attack, weight);
            }
        }
        void SetAttackStats()
        {
            attackTypeNext = -1;
            switch (attackType)
            {
                default:
                    break;
                case Crossbow:
                    attackTimer = 180;
                    attackCooldown = 60;
                    damage = 60;
                    break;
                case BloodJavelin:
                    attackTimer = 60;
                    attackCooldown = 60;
                    damage = 80;
                    break;
                case BloodSpike:
                    attackTimer = 120;
                    attackCooldown = 60;
                    damage = 80;
                    break;
                case BloodOrb:
                    attackTimer = 60;
                    attackCooldown = 200;
                    damage = 60;
                    break;
                case BloodScepter:
                    attackTimer = 30;
                    attackCooldown = 120;
                    damage = 60;
                    break;
                case BloodSickle:
                    attackTimer = 330;
                    attackCooldown = 20;
                    damage = 60;
                    break;
                case BloodScythe:
                    attackTimer = 120;
                    attackCooldown = 95;
                    break;
                case BloodSword:
                    attackTimer = 60;
                    attackCooldown = 20;
                    damage = 80;
                    break;
            }
            blacklistedAttacks.Add(attackType);
            if (Main.masterMode || Main.getGoodWorld)
            {
                attackCooldown /= 2;
            }
        }
        void CycleAttack(Player player)
        {
            NPC.netUpdate = true;
            Vector2 center = NPC.Center;
            Vector2 targetPosition = player.Center;
            Vector2 toTarget = targetPosition - center;
            switch (attackType)
            {
                default:
                    break;
                case Crossbow:
                    DoCrossbowShoot(center, toTarget);
                    break;
                case BloodJavelin:
                    DoJavelinThrow(center, toTarget);
                    break;
                case BloodSpike:
                    DoBloodSpike(center, toTarget);
                    break;
                case BloodOrb:
                    DoBubbleSpread(center, toTarget);
                    break;
                case BloodScepter:
                    DoScepterCast(center, toTarget);
                    break;
                case BloodSickle:
                    DoSickleSummon(targetPosition);
                    break;
                case BloodScythe:
                    DoScytheSwing(targetPosition);
                    break;
                case BloodSword:
                    DoSwordDraw(player);
                    break;
            }
        }

        void DoCrossbowShoot(Vector2 center, Vector2 toTarget)
        {
            toTarget.Normalize();
            toTarget *= 16f;
            if (attackTimer % 30 == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, toTarget,
                    ModContent.ProjectileType<SilverBoltHostile>(), damage, 0f, Main.myPlayer);
            }
            bool eternityMode = false;
            if (AttacksEnabled && eternityMode)
            {
                int type = ModContent.ProjectileType<BloodArrowHostile>();
                if (attackTimer % 60 == 0)
                {
                    toTarget.Normalize();
                    toTarget *= 6f;
                    float numberProjectiles = 2; // 2 extra shots
                    float rotation = MathHelper.ToRadians(20);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = toTarget.RotatedBy(MathHelper.Lerp(-rotation, rotation,
                            i / (numberProjectiles - 1)));
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), center, perturbedSpeed,
                            type, damage, 6, Main.myPlayer);
                    }
                }
            }
        }
        void DoJavelinThrow(Vector2 center, Vector2 toTarget)
        {
            int type = ModContent.ProjectileType<BloodJavelinHostile>();
            if (attackTimer == 60)
            {
                toTarget.Normalize();
                toTarget *= 6f;
                if (Main.masterMode)
                {
                    float numberProjectiles = 2; // 2 extra shots
                    float rotation = MathHelper.ToRadians(20);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = toTarget.RotatedBy(MathHelper.Lerp(-rotation, rotation,
                            i / (numberProjectiles - 1)));
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), center, perturbedSpeed,
                            type, damage, 6, Main.myPlayer);
                    }
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, toTarget,
                    type, damage, 0f, Main.myPlayer);
            }
        }
        void DoBloodSpike(Vector2 center, Vector2 toTarget)
        {
            if (attackTimer == 10)
            {
                int numProj = 3;
                if (Main.masterMode)
                {
                    numProj += 3;
                }
                float rotation = MathHelper.ToRadians(20);
                toTarget.Normalize();
                toTarget *= 16;
                for (int i = 0; i < numProj; i++)
                {
                    Vector2 perturbedSpeed = toTarget.RotatedByRandom(rotation);
                    perturbedSpeed *= Main.rand.NextFloat(0.66f, 1f);
                    var needle = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), center,
                        perturbedSpeed, ModContent.ProjectileType<BloodNeedleHostile>(), damage, 6,
                        Main.myPlayer, 0, NPC.Center.X, NPC.Center.Y);
                    needle.timeLeft *= 3;
                    needle.friendly = false;
                }
            }
            else if (attackTimer >= 30)
            {
                float speed = 16f;
                var targetCenter = toTarget + center;
                int direction = (NPC.Center.X > targetCenter.X ? 1 : -1);
                Vector2 toPosition = targetCenter + new Vector2(150 * direction, 0);
                NPC.Track(toPosition, speed, speed);
            }
        }
        void DoBubbleSpread(Vector2 center, Vector2 toTarget)
        {
            if (attackTimer % 60 == 0)
            {
                int numProj = Main.rand.Next(2) + 4;
                if (Main.masterMode)
                {
                    numProj += Main.rand.Next(2) + 2;
                }
                float rotation = MathHelper.ToRadians(20);
                toTarget.Normalize();
                toTarget *= 16;
                for (int i = 0; i < numProj; i++)
                {
                    Vector2 perturbedSpeed = toTarget.RotatedBy(MathHelper.Lerp(-rotation, rotation,
                        i / (numProj - 1)));
                    perturbedSpeed *= Main.rand.NextFloat(0.66f, 1f);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), center, perturbedSpeed,
                        ModContent.ProjectileType<BloodBubbleHostile>(), damage, 6, Main.myPlayer);
                }
            }
        }
        void DoScepterCast(Vector2 center, Vector2 toTarget)
        {
            if (attackTimer % 10 == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 16f,
                    ModContent.ProjectileType<BloodWaveHostile>(), damage, 0f, Main.myPlayer);
            }
        }
        void DoSickleSummon(Vector2 targetCenter)
        {
            if (attackTimer % 30 == 0)
            {
                float rotation = MathHelper.ToRadians(20);
                float radius = 250f * Main.rand.NextFloat(0.8f, 1f);
                var pos = targetCenter + Vector2.One.RotatedByRandom(MathHelper.ToRadians(360)) * radius;
                var vector = targetCenter - pos;
                vector.Normalize();
                vector *= 6f;
                Vector2 perturbedSpeed = vector.RotatedByRandom(rotation);
                perturbedSpeed *= Main.rand.NextFloat(0.66f, 1f);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), pos, perturbedSpeed,
                    ModContent.ProjectileType<BloodSickleHostile>(), damage, 6, Main.myPlayer);
            }
        }
        void DoScytheSwing(Vector2 targetCenter)
        {
            if (attackTimer > 30)
            {
                float speed = 8f;
                int direction = (NPC.Center.X > targetCenter.X ? 1 : -1);
                Vector2 toPosition = targetCenter + new Vector2(100 * direction, 0);
                NPC.Track(toPosition, speed, speed);
            }
            if (attackTimer == 30)
            {
            }
        }
        void DoSwordDraw(Player player)
        {
            if (attackTimer == 0)
            {

            }
        }

        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            NPC.AddBuff(ModContent.BuffType<DeathBleed>(), 600);
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            NPC.AddBuff(ModContent.BuffType<DeathBleed>(), 600);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<DeathBleed>(), 600);
        }

        void KillProjectiles()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                var proj = Main.projectile[i];
                if (proj.type == ModContent.ProjectileType<BloodArrowHostile>() ||
                    //proj.type == ModContent.ProjectileType<BloodBubble>() ||
                    proj.type == ModContent.ProjectileType<BloodJavelinHostile>() ||
                    //proj.type == ModContent.ProjectileType<BloodNeedle>() ||
                    //proj.type == ModContent.ProjectileType<BloodScythe>() ||
                    proj.type == ModContent.ProjectileType<BloodSickleHostile>() ||
                    //proj.type == ModContent.ProjectileType<BloodSpike>() ||
                    //proj.type == ModContent.ProjectileType<BloodSword>() ||
                    proj.type == ModContent.ProjectileType<SilverBoltHostile>()
                    )
                {
                    proj.Kill();
                }
            }
        }

        //public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        //{
        //    Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture);
        //    SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        //    Vector2 drawOrigin = new(texture.Value.Width * 0.5f, NPC.height * 0.5f);
        //    Vector2 drawPos = NPC.Center - screenPos;
        //    if (attackTimer > 0)
        //    {
        //        switch (attackType)
        //        {
        //            default:
        //                if (phase2)
        //                {
        //                    frameX = 1;
        //                }
        //                else
        //                {
        //                    frameX = 0;
        //                }
        //                break;
        //            case BloodJavelin:
        //            case BloodOrb:
        //                if (phase2)
        //                {
        //                    frameX = 2;
        //                }
        //                else
        //                {
        //                    frameX = 0;
        //                }
        //                break;
        //            case BloodSickle:
        //                frameX = 3;
        //                break;
        //            case BloodScytheSwing:
        //                frameX = 4;
        //                break;
        //        }
        //    }

        //    if (++NPC.frameCounter >= 5 && !Main.gameInactive)
        //    {
        //        if (++frameY >= Main.npcFrameCount[Type])
        //        {
        //            frameY = 0;
        //        }
        //        NPC.frameCounter = 0;
        //    }
        //    Rectangle frame = new(100 * frameX, 100 * frameY, 100, 100);
        //    spriteBatch.Draw(texture.Value, drawPos, frame, drawColor, NPC.rotation, frame.Size() / 2f, NPC.scale, effects, 0f);
        //    return false;
        //}
    }
}
