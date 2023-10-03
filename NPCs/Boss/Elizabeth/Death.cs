using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.GrabBags;
using ShardsOfAtheria.Items.PetItems;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Projectiles.NPCProj.Elizabeth;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
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
        int damagedTimer = 0;

        //int frameX = 0;
        //int frameY = 0;

        Color TextColor = Color.Red;
        public bool BloodAttacksEnabled => NPC.life < NPC.lifeMax;

        public override string BossHeadTexture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            //Main.npcFrameCount[NPC.type] = 6;

            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
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
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            Music = MusicID.Boss1;
            NPC.value = 167900;
            NPC.npcSlots = 15f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.ShardsOfAtheria.NPCs.Death.Bestuary"))
            });
        }

        public override void OnSpawn(IEntitySource source)
        {
            NPC.SetEventFlagCleared(ref ShardsDownedSystem.summonedDeath, -1);
            if (!SoA.DownedSystem.slainDeath)
            {
                // This should almost always be the first code in AI() as it is responsible for finding the proper player target
                if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                {
                    NPC.TargetClosest();
                }

                Player player = Main.player[NPC.target];

                Vector2 spawnPos = new(0, 400);
                if (Main.rand.NextFloat() <= .5f)
                {
                    spawnPos.X *= -1;
                }
                NPC.position = player.position - spawnPos;

                bool isSlayer = player.IsSlayer();
                NPC.UseBossDialogueWithKey("Death",
                    isSlayer ? ShardsHelpers.SlayerSummonLine :
                    PreviouslySummoned ? ShardsHelpers.ReSummonLine :
                    ShardsHelpers.SummonLine, TextColor);
                NPC.localAI[0] = 1f;
            }
        }

        public override bool CheckDead()
        {
            KillProjectiles();
            attackType = 0;
            attackTimer = 0;
            attackCooldown = 0;
            //frameX = 1;
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
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DeathBossBag>()));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            LeadingConditionRule slayerMode = new(new IsSlayerMode());
            //LeadingConditionRule master = new(new Conditions.IsMasterMode());

            int[] drops = {
                ModContent.ItemType<BloodStainedCrossbow>(),
                ModContent.ItemType<BloodJavelin>(),
                ModContent.ItemType<BloodScepter>(),
            };

            notExpertRule.OnSuccess(new OneFromOptionsDropRule(4, 3, drops));

            //npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<DeathRelic>()));
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeathTrophy>(), 10));
            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<LifeCycleKeys>(), 4));

            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DeathSoulCrystal>()));

            if (ModLoader.TryGetMod("MagicStorage", out Mod magicStorage) && !ShardsDownedSystem.downedValkyrie)
            {
                notExpertRule.OnSuccess(ItemDropRule.Common(magicStorage.Find<ModItem>("ShadowDiamond").Type));
            }

            //master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<>()));
            //npcLoot.Add(master);

            // Finally add the leading rule
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodScythe>()));
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
        static bool PreviouslySummoned => ShardsDownedSystem.summonedDeath;

        public override void AI()
        {
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }

            Player player = Main.player[NPC.target];
            bool isSlayer = player.Slayer().slayerMode;

            // death drama
            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f; // increase our death timer.
                NPC.velocity = isSlayer ? new Vector2(0, 1) * 8f : Vector2.Zero;
                if (NPC.ai[3] == 2)
                {
                    NPC.UseBossDialogueWithKey("Death",
                        isSlayer ? ShardsHelpers.LastWords :
                        PreviouslySummoned ? ShardsHelpers.ReDefeatLine :
                        ShardsHelpers.DefeatLine, TextColor);
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

            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;
            Lighting.AddLight(NPC.Center, Color.Cyan.ToVector3());

            //bool transitioning = false;
            if (NPC.life <= NPC.lifeMax * 0.8 && !phase2)
            {
                //transitioning = true;
                attackCooldown = 120;
                attackTimer = 0;
                attackType = -1;
                NPC.UseBossDialogueWithKey("Death",
                    isSlayer ? ShardsHelpers.SlayerMidFightLine :
                    PreviouslySummoned ? ShardsHelpers.ReMidFightLine :
                    ShardsHelpers.MidFightLine, TextColor);
                phase2 = true;
            }

            if (NPC.Distance(player.Center) > 600)
            {
                NPC.Track(player.Center, 18, 18);
            }

            if (NPC.life <= NPC.lifeMax * 0.25 && !desperation && player.IsSlayer())
            {
                NPC.UseBossDialogueWithKey("Death", ShardsHelpers.DesperationLine, TextColor);
                desperation = true;
            }

            //if (!transitioning)
            //{
            if (++damagedTimer >= 15.ToSeconds() &&
                NPC.life >= NPC.lifeMax * 0.75f)
            {
                damagedTimer = 0;
                NPC.HitInfo hitInfo = new()
                {
                    Damage = 15,
                };
                NPC.StrikeNPC(hitInfo);
            }
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
                DefaultMovement(player);
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
            //}
            NPC.netUpdate = true;
        }

        int damage = 0;
        const int Crossbow = 0;
        const int BloodJavelin = 1;
        const int NeedleWave = 2;
        const int BloodOrb = 3;
        const int BloodScepter = 4;
        const int BloodSickle = 5;
        const int BloodScythe = 6;
        const int BloodSword = 7;
        readonly List<int> blacklistedAttacks = new();
        void ChoseAttacks()
        {
            WeightedRandom<int> random = new();
            AddNonBlacklistedAttack(ref random, Crossbow);
            if (BloodAttacksEnabled)
            {
                AddNonBlacklistedAttack(ref random, BloodJavelin);
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
                    AddNonBlacklistedAttack(ref random, NeedleWave);
                }
                if (Main.masterMode)
                {
                    AddNonBlacklistedAttack(ref random, BloodSword);
                }
                if (Main.rand.NextBool(5))
                {
                    Player player = Main.player[NPC.target];
                    Vector2 center = NPC.Center;
                    Vector2 targetPosition = player.Center;
                    Vector2 toTarget = targetPosition - center;
                    DoBloodBarrier(center, toTarget);
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
                    attackTimer = 120;
                    attackCooldown = 60;
                    damage = 20;
                    if (NPC.life <= NPC.lifeMax / 4 * 3)
                    {
                        attackTypeNext = BloodScepter;
                    }
                    break;
                case BloodJavelin:
                    attackTimer = 60;
                    attackCooldown = 60;
                    damage = 25;
                    if (Main.expertMode)
                    {
                        attackTypeNext = NeedleWave;
                    }
                    break;
                case NeedleWave:
                    attackTimer = 120;
                    attackCooldown = 60;
                    damage = 25;
                    if (NPC.life <= NPC.lifeMax / 4 * 3)
                    {
                        attackTypeNext = BloodOrb;
                    }
                    break;
                case BloodOrb:
                    attackTimer = 60;
                    attackCooldown = 200;
                    damage = 20;
                    blacklistedAttacks.Add(NeedleWave);
                    break;
                case BloodScepter:
                    attackTimer = 30;
                    attackCooldown = 120;
                    damage = 20;
                    if (Main.masterMode)
                    {
                        attackTypeNext = BloodSword;
                    }
                    break;
                case BloodSickle:
                    attackTimer = 330;
                    attackCooldown = 300;
                    damage = 20;
                    attackTypeNext = BloodScythe;
                    break;
                case BloodScythe:
                    attackTimer = 120;
                    attackCooldown = 95;
                    damage = 70;
                    blacklistedAttacks.Add(BloodSickle);
                    break;
                case BloodSword:
                    attackTimer = 180;
                    attackCooldown = 60;
                    damage = 50;
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
                    DefaultMovement(player);
                    DoCrossbowShoot(center, toTarget);
                    break;
                case BloodJavelin:
                    DefaultMovement(player);
                    DoJavelinThrow(center, toTarget);
                    break;
                case NeedleWave:
                    DoNeedleWave(center, toTarget);
                    break;
                case BloodOrb:
                    DefaultMovement(player);
                    DoBubbleSpread(center, toTarget);
                    break;
                case BloodScepter:
                    DefaultMovement(player);
                    DoScepterCast(center, toTarget);
                    break;
                case BloodSickle:
                    DoSickleSummon(player);
                    break;
                case BloodScythe:
                    DoScytheSwing(targetPosition);
                    break;
                case BloodSword:
                    DoSwordDraw(player);
                    break;
            }
        }

        void DefaultMovement(Player player)
        {
            Vector2 idlePos = new(375, 0);
            if (player.Center.X > NPC.Center.X)
            {
                idlePos.X *= -1;
            }
            idlePos += player.Center;
            float speed = 16f;
            if (NPC.Distance(idlePos) > 300)
            {
                speed += 4f;
            }
            NPC.Track(idlePos, speed, 16f);
        }

        void DoCrossbowShoot(Vector2 center, Vector2 toTarget)
        {
            toTarget += Main.player[NPC.target].velocity * 16;
            toTarget.Normalize();
            toTarget *= 16f;
            if (attackTimer % 30 == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, toTarget,
                    ModContent.ProjectileType<SilverBoltHostile>(), damage, 0f, Main.myPlayer);
            }
            if (BloodAttacksEnabled && SoA.Eternity())
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
                toTarget *= 8f;
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
        void DoNeedleWave(Vector2 center, Vector2 toTarget)
        {
            if (attackTimer >= 10)
            {
                float speed = 16f;
                if (NPC.Distance(toTarget + center) > 100)
                {
                    speed += 6f;
                }
                var targetCenter = toTarget + center;
                int direction = (NPC.Center.X > targetCenter.X ? 1 : -1);
                Vector2 toPosition = targetCenter + new Vector2(150 * direction, 0);
                NPC.Track(toPosition, speed, speed);
            }
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
                        Main.myPlayer, NPC.Center.X, NPC.Center.Y);
                    needle.timeLeft *= 3;
                    needle.friendly = false;
                }
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
                toTarget += Main.player[NPC.target].velocity * 16;
                toTarget.Normalize();
                toTarget *= 16f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, toTarget,
                    ModContent.ProjectileType<BloodWaveHostile>(), damage, 0f,
                    Main.myPlayer);
            }
        }
        void DoSickleSummon(Player player)
        {
            Vector2 idlePos = new(0, -400);
            idlePos += player.Center;
            NPC.Track(idlePos, 16f, 16f);

            if (attackTimer % 30 == 0)
            {
                int amount = 1;
                if (Main.masterMode)
                {
                    amount = 2;
                }
                for (int i = 0; i < amount; i++)
                {
                    Vector2 targetCenter = player.Center;
                    float radius = 250f * Main.rand.NextFloat(0.8f, 1f);
                    var pos = targetCenter + Vector2.One.RotatedByRandom(MathHelper.ToRadians(360)) * radius;
                    var vector = targetCenter - pos;
                    vector.Normalize();
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center,
                        Vector2.Zero, ModContent.ProjectileType<BloodSickleSummon>(),
                        damage, 6, Main.myPlayer, pos.X - targetCenter.X,
                        pos.Y - targetCenter.Y, player.whoAmI);
                }
            }
        }
        void DoScytheSwing(Vector2 targetCenter)
        {
            float speed = 16f;
            if (NPC.Distance(targetCenter) > 100)
            {
                speed += 6f;
            }
            int direction = (NPC.Center.X > targetCenter.X ? 1 : -1);
            Vector2 toPosition = targetCenter + new Vector2(100 * direction, 0);
            NPC.Track(toPosition, speed, speed);
            if (attackTimer == 90)
            {
                var vector = targetCenter - NPC.Center;
                vector.Y = 0;
                vector.Normalize();
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center,
                    vector, ModContent.ProjectileType<BloodScytheHostile>(), damage,
                    0, Main.myPlayer, NPC.whoAmI);
            }
        }
        void DoSwordDraw(Player player)
        {
            float speed = 16f;
            if (NPC.Distance(player.Center) > 100)
            {
                speed += 6f;
            }
            NPC.Track(player.Center, speed, speed);
            if (NPC.Distance(player.Center) <= 150)
            {
                var vector = player.Center - NPC.Center;
                vector.Normalize();
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vector,
                    ModContent.ProjectileType<BloodSwordHostile>(), damage, 0f,
                    Main.myPlayer, NPC.whoAmI);
                attackTimer = 0;
            }
        }
        void DoBloodBarrier(Vector2 center, Vector2 toTarget)
        {
            toTarget.Normalize();
            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, toTarget,
                ModContent.ProjectileType<BloodBarrierHostile>(), damage, 0f,
                Main.myPlayer, NPC.whoAmI);
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (NPC.life <= NPC.lifeMax * 0.25f)
            {
                if (projectile.type == ModContent.ProjectileType<BloodArrowHostile>() ||
                    projectile.type == ModContent.ProjectileType<BloodJavelinHostile>() ||
                    projectile.type == ModContent.ProjectileType<BloodNeedleHostile>() ||
                    projectile.type == ModContent.ProjectileType<BloodSickleHostile>())
                {
                    return false;
                }
            }
            return base.CanBeHitByProjectile(projectile);
        }

        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            OnHitEffect();
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            OnHitEffect();
        }
        private void OnHitEffect()
        {
            damagedTimer = 0;
            NPC.AddBuff(ModContent.BuffType<DeathBleed>(), 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff<DeathBleed>(300);
        }

        private static void KillProjectiles()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                var proj = Main.projectile[i];
                if (proj.type == ModContent.ProjectileType<BloodArrowHostile>() ||
                    proj.type == ModContent.ProjectileType<BloodBubbleHostile>() ||
                    proj.type == ModContent.ProjectileType<BloodJavelinHostile>() ||
                    proj.type == ModContent.ProjectileType<BloodNeedleHostile>() ||
                    proj.type == ModContent.ProjectileType<BloodSickleHostile>() ||
                    proj.type == ModContent.ProjectileType<BloodScytheHostile>() ||
                    proj.type == ModContent.ProjectileType<BloodSwordHostile>() ||
                    proj.type == ModContent.ProjectileType<SilverBoltHostile>())
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
