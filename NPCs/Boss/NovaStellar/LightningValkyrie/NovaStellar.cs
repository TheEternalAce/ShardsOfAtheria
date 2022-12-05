using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.ItemDropRules.Condition;
using ShardsOfAtheria.ItemDropRules.Conditions;
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
using ShardsOfAtheria.Items.Weapons.Throwing;
using ShardsOfAtheria.NPCs.Town;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;
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

namespace ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie
{
    [AutoloadBossHead]
    public class NovaStellar : ModNPC
    {
        public int attackType = 0;
        public int attackTimer = 0;
        public int attackCooldown = 40;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];

            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    ModContent.BuffType<ElectricShock>(),

                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            SoAGlobalNPC.ElectricNPC.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 86;
            NPC.height = 60;
            NPC.damage = 30;
            NPC.defense = 8;
            NPC.lifeMax = 5200;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.boss = true;
            NPC.noGravity = true;
            Music = MusicID.Boss4;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.npcSlots = 15f;
            AnimationType = NPCID.Harpy;
            NPC.SetElementEffectivenessMultipliers(2.0, 0.8, 0.8, 1.5);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("Following her late father's footsteps, Nova became a noble knight. She wishes to rid the world of the Slayer Terrarian.")
            });
        }

        public override bool CheckDead()
        {
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

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            LeadingConditionRule slayerMode = new LeadingConditionRule(new IsSlayerMode());
            LeadingConditionRule flawless = new(new FlawlessDropCondition());

            int[] drops = { ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<ValkyrieCrown>(), ModContent.ItemType<DownBow>(), ModContent.ItemType<PlumeCodex>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, drops));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HardlightKnife>(), 5, 300, 600));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.GoldBar, 1, 10, 20));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ChargedFeather>(), 1, 15, 28));
            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieSoulCrystal>()));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<NovaRelic>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NovaTrophy>(), 10));
            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<SmallHardlightCrest>(), 25));

            if (ModLoader.TryGetMod("MagicStorage", out Mod magicStorage) && !ShardsDownedSystem.downedValkyrie)
            {
                notExpertRule.OnSuccess(ItemDropRule.Common(magicStorage.Find<ModItem>("ShadowDiamond").Type));
            }

            flawless.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieStormLance>()));
            npcLoot.Add(flawless);

            // Finally add the leading rule
            npcLoot.Add(notExpertRule);
            npcLoot.Add(slayerMode);
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref ShardsDownedSystem.downedValkyrie, -1);
            if (Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
            {
                ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie = true;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
            }
        }

        public override bool PreAI()
        {
            if (ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Nova Stellar was slain..."), Color.White);
                NPC.active = false;
            }
            return base.PreAI();
        }

        private bool midFight = false;
        private bool desperation = false;

        public override void AI()
        {
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];
            bool isSlayer = player.GetModPlayer<SlayerPlayer>().slayerMode;

            // death drama
            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f; // increase our death timer.
                NPC.velocity = Vector2.Zero;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    if (proj.type == ModContent.ProjectileType<StormSword>() || proj.type == ModContent.ProjectileType<StormLance>() || proj.type == ModContent.ProjectileType<FeatherBlade>()
                        || proj.type == ModContent.ProjectileType<StormCloud>())
                    {
                        proj.Kill();
                    }
                }
                if (NPC.ai[3] == 2)
                {
                    UseDialogue(isSlayer ? 7 : 3);
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
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }

            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie)
            {
                if (Main.rand.NextFloat() <= .5f)
                    NPC.position = player.position - new Vector2(500, 250);
                else NPC.position = player.position - new Vector2(-500, 250);
                UseDialogue(isSlayer ? 4 : 1);
                NPC.localAI[0] = 1f;
            }
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;

            if (NPC.life <= NPC.lifeMax * 0.5 && !midFight)
            {
                UseDialogue(isSlayer ? 5 : 2);
                midFight = true;
            }

            if (NPC.life <= NPC.lifeMax * 0.25 && !desperation && player.GetModPlayer<SlayerPlayer>().slayerMode)
            {
                UseDialogue(6);
                desperation = true;
            }

            if (attackCooldown <= 0)
            {
                if (NPC.life <= NPC.lifeMax / 4 * 3)
                {
                    attackType = Main.rand.Next(4);
                    if (NPC.life <= NPC.lifeMax / 2)
                    {
                        attackType = Main.rand.Next(5);
                    }
                }
                else
                {
                    attackType = Main.rand.Next(3);
                }

                switch (attackType)
                {
                    default:
                        break;
                    case 0:
                        // Electric Dash
                        attackTimer = 60;
                        attackCooldown = 60;
                        break;
                    case 1:
                        // Feather Blade Barrage
                        attackTimer = 240;
                        attackCooldown = 60;
                        break;
                    case 2:
                        // Lance Dash
                        attackTimer = 120;
                        attackCooldown = 60;
                        break;
                    case 3:
                        // Storm Cloud
                        attackTimer = 320;
                        attackCooldown = 60;
                        break;
                    case 4:
                        // Sword Dance
                        attackTimer = 9 * 60;
                        attackCooldown = 60;
                        break;
                }
            }

            if (attackTimer > 0)
            {
                NPC.netUpdate = true;
                Vector2 center = NPC.Center;
                Vector2 targetPosition = player.Center;
                Vector2 toTarget = targetPosition - center;
                switch (attackType)
                {
                    default:
                        break;
                    case 0:
                        // Electric Dash
                        if (attackTimer == 60)
                        {
                            NPC.velocity = Vector2.Normalize(toTarget) * 10;
                        }
                        if (attackTimer % 2 == 0)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Zero, ModContent.ProjectileType<ElectricTrail>(), 18, 0f, Main.myPlayer);
                        }
                        if (NPC.life < NPC.lifeMax / 2 && attackTimer == 1)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 1f;
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, velocity, ModContent.ProjectileType<LightningBolt>(), 18, 0f, Main.myPlayer);
                            }
                        }
                        break;
                    case 1:
                        // Feather Blade Barrage
                        if (attackTimer == 240)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                        }
                        // + pattern
                        if (attackTimer < 240 && attackTimer > 180)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 dustPos = targetPosition + new Vector2(1.425f, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                                Dust dust = Dust.NewDustPerfect(dustPos, DustID.Electric);
                                dust.noGravity = true;
                            }
                        }
                        if (attackTimer == 180)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 projPos = targetPosition + new Vector2(1.425f, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos, Vector2.Normalize(targetPosition - projPos) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, 0, 60f);
                            }
                        }
                        if (attackTimer == 120)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                        }
                        // X pattern
                        if (attackTimer < 120 && attackTimer > 60)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 dustPos = targetPosition + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150f;
                                Dust dust = Dust.NewDustPerfect(dustPos, DustID.Electric);
                                dust.noGravity = true;
                            }
                        }
                        if (attackTimer == 60)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 projPos = targetPosition + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150f;
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos, Vector2.Normalize(targetPosition - projPos) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, 0, 60f);
                            }
                        }
                        break;
                    case 2:
                        // Lance Dash
                        NPC.rotation = 0;
                        if (attackTimer > 30)
                        {
                            NPC.velocity = Vector2.Normalize(player.Center + new Vector2(250 * (NPC.Center.X > player.Center.X ? 1 : -1), 0) - NPC.Center) * 12;
                        }
                        if (attackTimer == 30)
                        {
                            NPC.velocity = Vector2.Normalize(player.Center + player.velocity * 12 - center) * 16;
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, NPC.velocity * 0.75f, ModContent.ProjectileType<StormLance>(), 16, 0, Main.myPlayer);
                        }
                        break;
                    case 3:
                        // Storm Cloud
                        if (attackTimer > 160)
                        {
                            NPC.velocity = Vector2.Normalize(player.Center + new Vector2(500 * (NPC.Center.X > player.Center.X ? 1 : -1), -200) - NPC.Center) * 12;
                        }
                        else
                        {
                            NPC.Center = player.Center + new Vector2(500 * (NPC.Center.X > player.Center.X ? 1 : -1), -200);
                        }
                        if (attackTimer == 320)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(0, -400), Vector2.Zero, ModContent.ProjectileType<StormCloud>(), 16, 0, Main.myPlayer);
                        }
                        if (attackTimer == 1)
                        {
                            NPC.velocity = Vector2.Zero;
                        }
                        break;
                    case 4:
                        // Sword Dance
                        if (attackTimer > 8 * 60)
                        {
                            NPC.velocity = Vector2.Normalize(player.Center + new Vector2(500 * (NPC.Center.X > player.Center.X ? 1 : -1), -200) - NPC.Center) * 12;
                        }
                        else
                        {
                            NPC.Center = player.Center + new Vector2(500 * (NPC.Center.X > player.Center.X ? 1 : -1), -200);
                        }
                        if (attackTimer == 9 * 60)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center, Vector2.Zero, ModContent.ProjectileType<StormSword>(), 16, 0, Main.myPlayer, i);
                            }
                        }
                        if (attackTimer == 1)
                        {
                            NPC.velocity = Vector2.Zero;
                        }
                        break;
                }
                attackTimer--;
            }
            else
            {
                // decrease wait timer when attack done
                attackCooldown--;
            }

            NPC.netUpdate = true;
        }

        void UseDialogue(int index)
        {
            string dialogue;

            switch (index)
            {
                default: // Testing
                    dialogue = "Placeholder Text";
                    break;

                case 1: // Initial summon
                    dialogue = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.InitialSummon");
                    break;
                case 2: // Mid fight
                    dialogue = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.MidFight");
                    break;
                case 3: // Defeat
                    int atherian = NPC.FindFirstNPC(ModContent.NPCType<Atherian>());
                    if (atherian >= 0)
                    {
                        dialogue = string.Format(Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.Defeat", Main.npc[atherian].GivenName));
                    }
                    else
                    {
                        dialogue = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.Defeat2");
                    }
                    break;

                case 4: // Slayer mode initial summon
                    dialogue = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.InitialSummonAlt");
                    break;
                case 5: // Slayer mode mid fight
                    dialogue = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.MidFightAlt");
                    break;
                case 6: // Slayer mode 25% life
                    dialogue = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.Desperation");
                    break;
                case 7: // Slayer mode defeat
                    dialogue = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.Death");
                    break;
            }

            int text = CombatText.NewText(NPC.getRect(), Color.DeepSkyBlue, dialogue, index == 6 || index == 5);
            Main.combatText[text].lifeTime = 150;
        }

        public override void DrawEffects(ref Color drawColor)
        {
            if (midFight && Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(NPC.position, NPC.width + 4, NPC.height + 4, DustID.Electric, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
            if (desperation && Main.rand.NextBool(8))
            {
                int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, DustID.Blood, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }
}
