using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.GrabBags;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.PetItems;
using ShardsOfAtheria.Items.Placeable.Furniture.Trophies;
using ShardsOfAtheria.Items.Placeable.Furniture.Trophies.Master;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Vanity;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie
{
    [AutoloadBossHead]
    public class NovaStellar : ModNPC
    {
        int attackType = 0;
        int attackTimer = 0;
        int attackCooldown = 40;
        int attackTypeNext = -1;

        int frameX = 0;
        int frameY = 0;

        Color TextColor = Color.DeepSkyBlue;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;

            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;

            List<int> buffTypes = [
                BuffID.Poisoned,
                BuffID.Confused,
                ModContent.BuffType<ElectricShock>()
            ];
            NPC.SetImmuneTo(buffTypes);

            NPC.AddDamageType(5);
            NPC.AddVulnerabilities("weak", 3);
            NPC.AddVulnerabilities("resist", 2, 5, 12);

            NPC.AddElement(2);

            NPC.AddRedemptionElement(7);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Armed");
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 38;
            NPC.damage = 30;
            NPC.defense = 8;
            NPC.lifeMax = 5200;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            Music = MusicID.Boss4;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.npcSlots = 15f;

            NPC.ElementMultipliers([2.0f, 0.8f, 0.8f, 1.5f]);

            NPC.SetDebuffResistance("Electricity", false);
            NPC.SetDebuffResistance("Heat", true);
            NPC.SetDebuffResistance("Sickness", true);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            string key = this.GetLocalizationKey("Bestiary");
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement(key)
            });
        }

        public override void OnSpawn(IEntitySource source)
        {
            NPC.SetEventFlagCleared(ref ShardsDownedSystem.summonedValkyrie, -1);
            if (!SoA.DownedSystem.slainValkyrie)
            {
                // This should almost always be the first code in AI() as it is responsible for finding the proper player target
                if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                    NPC.TargetClosest();

                Player player = Main.player[NPC.target];

                Vector2 spawnPos = new(500, 250);
                if (Main.rand.NextFloat() <= .5f) spawnPos.X *= -1;
                NPC.position = player.position - spawnPos;

                bool isSlayer = player.IsSlayer();
                NPC.UseBossDialogueWithKey("NovaStellar",
                    isSlayer ? ShardsHelpers.SlayerSummonLine :
                    AlreadyDefeated ? ShardsHelpers.ReSummonLine :
                    ShardsHelpers.SummonLine, TextColor);
                NPC.localAI[0] = 1f;

                if (SoA.Eternity())
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<OrbitingHardlightBlade>(), 10, 0, NPC.whoAmI, i);
                    }
                }
            }
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
            LeadingConditionRule masterOrEternity = new(new EternityOrMaster());

            int[] drops = [ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<DownBow>(), ModContent.ItemType<PlumeCodex>(), ModContent.ItemType<NestlingStaff>()];

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, drops));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HardlightKnife>(), 5, 150, 180));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieCrown>(), 5));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HardlightPrism>(), 1, 15, 28));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ThunderValkyrieMask>(), 7));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.GoldBar, 1, 8, 14));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<NovaRelic>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NovaTrophy>(), 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ThunderValkyrieMask>(), 10));
            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<SmallHardlightCrest>(), 4));

            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieSoulCrystal>()));

            if (ShardsHelpers.TryGetModContent("MagicStorage", "ShadowDiamond", out ModItem diamond))
            {
                LeadingConditionRule alreadyDowned = new(new DownedValkyrie());
                alreadyDowned.OnFailedConditions(ItemDropRule.Common(diamond.Type));
            }

            if (ShardsHelpers.TryGetModContent("FargowiltasSouls", "WyvernFeather", out ModItem feather))
            {
                LeadingConditionRule eternity = new(new Eternity());
                eternity.OnSuccess(ItemDropRule.Common(feather.Type, 20));
                npcLoot.Add(eternity);
            }

            masterOrEternity.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieStormLance>()));

            // Finally add the leading rule
            npcLoot.Add(notExpertRule);
            npcLoot.Add(slayerMode);
            npcLoot.Add(masterOrEternity);
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref ShardsDownedSystem.downedValkyrie, -1);
            Player lastPlayerToHitThisNPC = NPC.AnyInteractions() ? Main.player[NPC.lastInteraction] : null;
            if (lastPlayerToHitThisNPC != null)
            {
                if (lastPlayerToHitThisNPC.IsSlayer())
                {
                    NPC.SlayNPC(lastPlayerToHitThisNPC);
                    SoA.DownedSystem.slainValkyrie = true;
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height,
                            DustID.Electric);
                        d.noGravity = true;
                        d.velocity *= 2;
                    }
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode)
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
            if (SoA.Eternity() && Main.rand.NextBool(5))
                target.AddBuff(ModContent.Find<ModBuff>("FargowiltasSouls", "ClippedWingsBuff").Type, 600);
        }

        public override bool PreAI()
        {
            if (SoA.DownedSystem.slainValkyrie)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Nova Stellar was slain..."), Color.White);
                NPC.active = false;
            }
            return base.PreAI();
        }

        private bool Phase2 => NPC.life <= NPC.lifeMax / 2;
        private bool phase2Transition = false;
        private bool Desperation => Main.player[NPC.target].Slayer().slayerMode && NPC.life <= NPC.lifeMax / 4;
        bool AlreadyDefeated = ShardsDownedSystem.downedValkyrie;

        public override void AI()
        {
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                NPC.TargetClosest();

            Player player = Main.player[NPC.target];
            bool isSlayer = player.Slayer().slayerMode;

            // death drama
            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f; // increase our death timer.
                NPC.velocity = isSlayer ? new Vector2(0, 1) * 8f : Vector2.Zero;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    if (proj.type == ModContent.ProjectileType<StormSword>() || proj.type == ModContent.ProjectileType<StormLance>() || proj.type == ModContent.ProjectileType<FeatherBlade>()
                        || proj.type == ModContent.ProjectileType<StormCloud>())
                        proj.Kill();
                }
                if (NPC.ai[3] == 2)
                {
                    NPC.UseBossDialogueWithKey("NovaStellar",
                        isSlayer ? ShardsHelpers.LastWords :
                        AlreadyDefeated ? ShardsHelpers.ReDefeatLine :
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

            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !SoA.DownedSystem.slainValkyrie)
            {
                if (Main.rand.NextFloat() <= .5f)
                    NPC.position = player.position - new Vector2(500, 250);
                else NPC.position = player.position - new Vector2(-500, 250);
                NPC.UseBossDialogueWithKey("NovaStellar",
                    isSlayer ? ShardsHelpers.SlayerSummonLine :
                    AlreadyDefeated ? ShardsHelpers.ReSummonLine :
                    ShardsHelpers.SummonLine, TextColor);
                NPC.localAI[0] = 1f;
            }
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;

            bool transitioning = false;
            if (Phase2 && !phase2Transition)
            {
                transitioning = true;
                attackCooldown = 120;
                attackTimer = 0;
                attackType = -1;
                DoPhase2Transition();
            }

            if (Desperation && player.Slayer().slayerMode)
                NPC.UseBossDialogueWithKey("NovaStellar", ShardsHelpers.DesperationLine, Color.Red);

            if (!transitioning)
            {
                if (attackCooldown <= 0) ChoseAttacks();

                if (attackTimer > 0)
                {
                    CycleAttack(player);
                    attackTimer--;
                }
                else
                {
                    if (attackType != LanceDash) DefaultMovement(player);
                    if (Phase2) frameX = 1;
                    else frameX = 0;
                    // decrease wait timer when attack done
                    attackCooldown--;
                }
            }
            NPC.netUpdate = true;
        }

        int damage = 0;
        const int ElectricDash = 0;
        const int FeatherBarrage = 1;
        const int LanceDash = 2;
        const int StormCloud = 3;
        const int SwordsDance = 4;
        const int BowShoot = 5;
        const int SwordSwing = 6;
        const int KnifeThrow = 7;
        const int Teleport = 8;
        readonly List<int> blacklistedAttacks = [];
        void ChoseAttacks()
        {
            WeightedRandom<int> random = new();
            AddNonBlacklistedAttack(ref random, ElectricDash);
            AddNonBlacklistedAttack(ref random, FeatherBarrage);
            if (NPC.life <= NPC.lifeMax / 4 * 3)
            {
                if (Phase2)
                {
                    AddNonBlacklistedAttack(ref random, BowShoot);
                    AddNonBlacklistedAttack(ref random, SwordSwing);
                }
                AddNonBlacklistedAttack(ref random, StormCloud);
                AddNonBlacklistedAttack(ref random, SwordsDance);
            }
            if (Main.expertMode) AddNonBlacklistedAttack(ref random, KnifeThrow);
            if (Main.masterMode) AddNonBlacklistedAttack(ref random, LanceDash);
            if (SoA.Eternity()) AddNonBlacklistedAttack(ref random, Teleport);
            if (Desperation)
            {

            }
            attackType = random;
            if (attackTypeNext > -1) attackType = attackTypeNext;
            blacklistedAttacks.Clear();
            SetAttackStats();
        }
        void AddNonBlacklistedAttack(ref WeightedRandom<int> randomAttack, int attack, double weight = 1)
        {
            if (!blacklistedAttacks.Contains(attack)) randomAttack.Add(attack, weight);
        }
        void SetAttackStats()
        {
            attackTypeNext = -1;
            switch (attackType)
            {
                default:
                    break;
                case ElectricDash:
                    attackTimer = 60;
                    attackCooldown = 60;
                    damage = 18;
                    if (Phase2) attackTypeNext = BowShoot;
                    break;
                case FeatherBarrage:
                    attackTimer = 260;
                    attackCooldown = 60;
                    damage = 14;
                    blacklistedAttacks.Add(StormCloud);
                    break;
                case LanceDash:
                    attackTimer = 120;
                    attackCooldown = 10;
                    damage = 20;
                    attackTypeNext = KnifeThrow;
                    break;
                case StormCloud:
                    attackTimer = 340;
                    attackCooldown = 60;
                    damage = 18;
                    attackTypeNext = FeatherBarrage;
                    break;
                case SwordsDance:
                    attackTimer = 9 * 60;
                    attackCooldown = 60;
                    damage = 14;
                    if (Phase2) attackTypeNext = SwordSwing;
                    break;
                case BowShoot:
                    attackTimer = 56;
                    attackCooldown = 20;
                    damage = 11;
                    blacklistedAttacks.Add(ElectricDash);
                    break;
                case SwordSwing:
                    attackTimer = 360;
                    attackCooldown = 20;
                    blacklistedAttacks.Add(SwordsDance);
                    break;
                case KnifeThrow:
                    attackTimer = 60;
                    attackCooldown = 20;
                    damage = 10;
                    blacklistedAttacks.Add(LanceDash);
                    break;
                case Teleport:
                    attackTimer = 10;
                    attackCooldown = 40;
                    damage = 12;
                    if (Phase2) attackTypeNext = BowShoot;
                    break;
            }
            blacklistedAttacks.Add(attackType);
            if (SoA.Massochist()) attackCooldown /= 4;
            else if (SoA.Eternity()) attackCooldown /= 2;
            else if (Main.masterMode || Main.getGoodWorld) attackCooldown = attackCooldown * 3 / 4;
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
                case ElectricDash:
                    DoElectricDash(center, toTarget);
                    break;
                case FeatherBarrage:
                    DefaultMovement(player);
                    DoFeatherBarrage(center, toTarget, targetPosition);
                    break;
                case LanceDash:
                    DoLanceDash(player, targetPosition, center);
                    break;
                case StormCloud:
                    DefaultMovement(player);
                    DoStormCloud(targetPosition);
                    break;
                case SwordsDance:
                    DefaultMovement(player);
                    DoSwordsDance(targetPosition);
                    break;
                case BowShoot:
                    DefaultMovement(player);
                    DoBowShoot(center, toTarget);
                    break;
                case SwordSwing:
                    DoBladeSwing(player);
                    break;
                case KnifeThrow:
                    DefaultMovement(player);
                    DoKnifeThrow(player);
                    break;
                case Teleport:
                    DoTeleport(targetPosition);
                    break;
            }
        }

        void DefaultMovement(Player player)
        {
            Vector2 idlePos = new(250, -125);
            if (player.Center.X > NPC.Center.X) idlePos.X *= -1;
            idlePos += player.Center;
            var vectorToIdlePos = idlePos - NPC.Center;
            NPC.velocity = vectorToIdlePos * 0.055f;
        }

        void DoElectricDash(Vector2 center, Vector2 toTarget)
        {
            if (attackTimer == 60) NPC.velocity = Vector2.Normalize(toTarget) * 10;
            if (attackTimer % 2 == 0)
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Zero, ModContent.ProjectileType<ElectricTrail>(),
                    damage, 0f, Main.myPlayer);
            if (NPC.life < NPC.lifeMax / 2 && attackTimer == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 velocity = Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 1f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), center, velocity, ModContent.ProjectileType<LightningBolt>(),
                        damage, 0f, Main.myPlayer);
                }
            }
        }

        void DoFeatherBarrage(Vector2 center, Vector2 toTarget, Vector2 targetPosition)
        {
            if (attackTimer > 240) return;
            if (attackTimer == 240)
            {
                SoundEngine.PlaySound(SoundID.Item43, center);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 7f,
                    ModContent.ProjectileType<FeatherBlade>(), damage, 0f, Main.myPlayer);
            }
            // + pattern
            if (!SoA.Eternity())
            {
                if (attackTimer < 240 && attackTimer > 180)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 dustPos = targetPosition + new Vector2(1.425f, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.Electric);
                        dust.noGravity = true;
                    }
                }
            }
            // The actual attack
            if (attackTimer == 180)
            {
                SoundEngine.PlaySound(SoundID.Item43, center);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projPos = targetPosition + new Vector2(1.425f, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos, Vector2.Normalize(targetPosition - projPos) * 7f,
                        ModContent.ProjectileType<FeatherBlade>(), damage, 0f, Main.myPlayer, 1f);
                }
            }
            if (attackTimer == 120)
            {
                SoundEngine.PlaySound(SoundID.Item43, center);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 7f,
                    ModContent.ProjectileType<FeatherBlade>(), damage, 0f, Main.myPlayer);
            }
            // X pattern
            if (!SoA.Eternity())
            {
                if (attackTimer < 120 && attackTimer > 60)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 dustPos = targetPosition + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.Electric);
                        dust.noGravity = true;
                    }
                }
            }
            // the actual attack
            if (attackTimer == 60)
            {
                SoundEngine.PlaySound(SoundID.Item43, center);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projPos = targetPosition + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos, Vector2.Normalize(targetPosition - projPos) * 7f,
                        ModContent.ProjectileType<FeatherBlade>(), damage, 0f, Main.myPlayer, 1f);
                }
            }
        }

        void DoLanceDash(Player player, Vector2 targetPosition, Vector2 center)
        {
            if (attackTimer > 30)
            {
                int direction = (NPC.Center.X > targetPosition.X ? 1 : -1);
                Vector2 toPosition = targetPosition + new Vector2(150 * direction, 0);
                var vectorToIdlePos = toPosition - NPC.Center;
                NPC.velocity = vectorToIdlePos * 0.055f;
            }
            if (attackTimer == 30)
            {
                Vector2 playerY = new(0, player.velocity.Y);
                NPC.velocity = Vector2.Normalize(targetPosition + playerY * 12 - center) * 16;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, NPC.velocity * 0.75f,
                    ModContent.ProjectileType<StormLance>(), damage, 0, Main.myPlayer);
            }
        }

        void DoStormCloud(Vector2 targetPosition)
        {
            if (attackTimer == 320)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), targetPosition + new Vector2(0, -400), Vector2.Zero,
                    ModContent.ProjectileType<StormCloud>(), damage, 0, Main.myPlayer);
            }
            if (Main.getGoodWorld) DoBowShoot(NPC.Center, targetPosition - NPC.Center);
        }

        void DoSwordsDance(Vector2 targetPosition)
        {
            if (attackTimer == 9 * 60)
            {
                for (int i = 0; i < 7; i++)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), targetPosition, Vector2.Zero,
                        ModContent.ProjectileType<StormSword>(), damage, 0, Main.myPlayer, i);
                }
            }
        }

        void DoBowShoot(Vector2 center, Vector2 toTarget)
        {
            Player player = Main.player[NPC.target];
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;
            bool shouldFire = attackTimer <= 36;
            if (attackTimer % 12 == 0)
            {
                if (attackType == StormCloud) shouldFire = Main.rand.NextBool(10);
                if (shouldFire)
                {
                    SoundEngine.PlaySound(SoundID.Item5, center);
                    float numberProjectiles = 3;
                    float rotation = MathHelper.ToRadians(5);
                    toTarget.Normalize();
                    center += Vector2.Normalize(toTarget) * 10f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = toTarget.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                        Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), center, perturbedSpeed * 16f,
                            ModContent.ProjectileType<FeatherBlade>(), damage, 3, Main.myPlayer);
                        proj.DamageType = DamageClass.Ranged;
                    }
                }
            }
        }

        void DoBladeSwing(Player player)
        {
            Vector2 vector = NPC.Center - player.Center;
            vector.Normalize();
            vector *= 50;
            var vectorToIdlePos = player.Center + vector - NPC.Center;
            NPC.velocity = vectorToIdlePos * 0.055f;
            if (frameY == 0 && NPC.frameCounter == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, NPC.Center);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center,
                    Vector2.Zero, ModContent.ProjectileType<HardlightBladeHitbox>(),
                    damage, 0, Main.myPlayer);
                if (SoA.Eternity())
                {
                    var vector1 = player.Center - NPC.Center;
                    vector1.Normalize();
                    vector1 *= 16f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vector1,
                        ModContent.ProjectileType<HardlightSlashHostile>(), damage, 0, Main.myPlayer);
                }
            }
        }

        void DoKnifeThrow(Player player)
        {
            if (attackTimer % 10 == 0)
            {
                var vectorToPlayer = player.Center - NPC.Center;
                vectorToPlayer.Normalize();
                vectorToPlayer = vectorToPlayer.RotatedByRandom(MathHelper.ToRadians(10));
                SoundEngine.PlaySound(SoundID.Item1, NPC.Center);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vectorToPlayer * 16f,
                    ModContent.ProjectileType<HardlightKnifeHostile>(), damage, 0, Main.myPlayer);
            }
        }

        void DoTeleport(Vector2 targetCenter)
        {
            if (attackTimer == 10)
            {
                bool validPosition = false;
                var teleportPosition = Vector2.Zero;
                while (!validPosition)
                {
                    teleportPosition = targetCenter + Main.rand.NextVector2CircularEdge(200, 200) * (1 - Main.rand.NextFloat(0.33f));
                    if (!Collision.SolidCollision(teleportPosition, NPC.width, NPC.height))
                    {
                        validPosition = true;
                        break;
                    }
                }
                if (validPosition)
                {
                    NPC.Center = teleportPosition;
                    NPC.netUpdate = true;
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 velocity = Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 1f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<LightningBolt>(),
                            damage, 0f, Main.myPlayer);
                    }
                }
            }
        }

        int transitionTime = 120;
        void DoPhase2Transition()
        {
            if (transitionTime == 120)
            {
                Player player = Main.player[NPC.target];
                bool isSlayer = player.Slayer().slayerMode;

                NPC.UseBossDialogueWithKey("NovaStellar",
                    isSlayer ? ShardsHelpers.SlayerMidFightLine :
                    AlreadyDefeated ? ShardsHelpers.ReMidFightLine :
                    ShardsHelpers.MidFightLine, TextColor);

                NPC.dontTakeDamage = true;
                KillProjectiles();
                SoundEngine.PlaySound(SoundID.Thunder, NPC.Center);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 velocity = Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 1f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<LightningBolt>(), 18, 0f, Main.myPlayer);
                }
            }
            NPC.velocity *= 0;
            attackCooldown = transitionTime;
            if (transitionTime > 100) frameX = 5;
            else frameX = 6;
            if (--transitionTime == 0)
            {
                phase2Transition = true;
                NPC.dontTakeDamage = false;
            }
        }

        static void KillProjectiles()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                var proj = Main.projectile[i];
                if (proj.type == ModContent.ProjectileType<ElectricTrail>() ||
                    proj.type == ModContent.ProjectileType<FeatherBlade>() ||
                    proj.type == ModContent.ProjectileType<LightningBolt>() ||
                    proj.type == ModContent.ProjectileType<StormSword>() ||
                    proj.type == ModContent.ProjectileType<StormCloud>() ||
                    proj.type == ModContent.ProjectileType<LightningBolt>() ||
                    proj.type == ModContent.ProjectileType<LightningBolt>() ||
                    proj.type == ModContent.ProjectileType<StormLance>() ||
                    proj.type == ModContent.ProjectileType<HardlightKnifeHostile>())
                {
                    proj.Kill();
                }
            }
        }

        public override void DrawEffects(ref Color drawColor)
        {
            if (Phase2 && Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(NPC.position, NPC.width + 4, NPC.height + 4, DustID.Electric, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
            if (Desperation && Main.rand.NextBool(8))
            {
                int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, DustID.Blood, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture + (SoA.Eternity() ? "_Eternity" : ""));
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 drawPos = NPC.Center - screenPos;
            if (attackTimer > 0)
            {
                switch (attackType)
                {
                    default:
                        if (Phase2) frameX = 1;
                        else frameX = 0;
                        break;
                    case FeatherBarrage:
                        if (Phase2) frameX = 2;
                        else frameX = 0;
                        break;
                    case StormCloud:
                        if (Phase2) frameX = 2;
                        else frameX = 0;
                        if (Main.getGoodWorld) frameX = 3;
                        break;
                    case BowShoot:
                        frameX = 3;
                        break;
                    case SwordSwing:
                        frameX = 4;
                        break;
                }
            }

            if (++NPC.frameCounter >= 5 && !Main.gameInactive)
            {
                if (++frameY >= Main.npcFrameCount[Type]) frameY = 0;
                NPC.frameCounter = 0;
            }
            Rectangle frame = new(100 * frameX, 100 * frameY, 100, 100);
            drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);
            Main.EntitySpriteDraw(texture.Value, NPC.Center - screenPos, frame, drawColor, NPC.rotation, frame.Size() / 2, NPC.scale, effects);
            return false;
        }
    }
}
