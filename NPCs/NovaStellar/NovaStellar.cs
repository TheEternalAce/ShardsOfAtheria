using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.ItemDropRules.Conditions;
using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;
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

namespace ShardsOfAtheria.NPCs.NovaStellar
{
    [AutoloadBossHead]
    public class NovaStellar : ModNPC
    {
        public int dialogueTimer;

        public int attackType = 0;
        public int attackTimer = 0;
        public int attackCooldown = 40;

        // This boss has a second phase and we want to give it a second boss head icon, this variable keeps track of the registered texture from Load().
        // It is applied in the BossHeadSlot hook when the boss is in its second stage
        public static int secondStageHeadSlot = -1;

        // This code here is called a property: It acts like a variable, but can modify other things. In this case it uses the NPC.ai[] array that has four entries.
        // We use properties because it makes code more readable ("if (SecondStage)" vs "if (NPC.ai[0] == 1f)").
        // We use NPC.ai[] because in combination with NPC.netUpdate we can make it multiplayer compatible. Otherwise (making our own fields) we would have to write extra code to make it work (not covered here)
        public bool SecondStage
        {
            get => NPC.ai[0] == 1f;
            set => NPC.ai[0] = value ? 1f : 0f;
        }
        // This is a reference property. It lets us write FirstStageTimer as if it's NPC.localAI[1], essentially giving it our own name
        public ref float FirstStageTimer => ref NPC.localAI[1];

        public ref float SecondStageTimer => ref NPC.localAI[2];

        // Do NOT try to use NPC.ai[4]/NPC.localAI[4] or higher indexes, it only accepts 0, 1, 2 and 3!
        // If you choose to go the route of "wrapping properties" for NPC.ai[], make sure they don't overlap (two properties using the same variable in different ways), and that you don't accidently use NPC.ai[] directly

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Stellar, the Lightning Valkyrie");
            Main.npcFrameCount[NPC.type] = 1;

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
        }

        public override void SetDefaults()
        {
            NPC.width = 86;
            NPC.height = 60;
            NPC.damage = 15;
            NPC.defense = 8;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.boss = true;
            NPC.noGravity = true;
            Music = MusicID.Boss4;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.npcSlots = 15f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("Bestiary entry in progress.")
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

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<ValkyrieCrown>(), ModContent.ItemType<DownBow>(), ModContent.ItemType<PlumeCodex>()));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.GoldBar, 1, 10, 20));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ChargedFeather>(), 1, 15, 28));
            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieSoulCrystal>()));
            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieStormLance>()));

            // Finally add the leading rule
            npcLoot.Add(notExpertRule);
            npcLoot.Add(slayerMode);
        }

        public override void OnKill()
        {
            if (ModLoader.TryGetMod("MagicStorage", out Mod magicStorage) && !SoADownedSystem.downedValkyrie)
            {
                Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), magicStorage.Find<ModItem>("ShadowDiamond").Type);
            }

            NPC.SetEventFlagCleared(ref SoADownedSystem.downedValkyrie, -1);
            if (Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
                ModContent.GetInstance<SoADownedSystem>().slainValkyrie = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode || SecondStage)
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }

        public override bool PreAI()
        {
            if (ModContent.GetInstance<SoADownedSystem>().slainValkyrie)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Nova Stellar, the Lightning Valkyrie was slain..."), Color.White);
                NPC.active = false;
            }
            return base.PreAI();
        }

        public override void AI()
        {
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            // death drama
            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f; // increase our death timer.
                                 //NPC.velocity = Vector2.UnitY * NPC.velocity.Length();
                NPC.velocity = Vector2.Zero;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    if (proj.type == ModContent.ProjectileType<StormSword>() || proj.type == ModContent.ProjectileType<StormLance>() || proj.type == ModContent.ProjectileType<FeatherBlade>())
                    {
                        proj.Kill();
                    }
                }
                //if (NPC.ai[3] > 1f && NPC.ai[3] == 2)
                //{
                //    if (Player.GetModPlayer<SlayerPlayer>().slayerMode)
                //        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("");
                //    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("");
                //}
                if (NPC.ai[3] >= 120f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                }
                return;
            }

            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }

            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !ModContent.GetInstance<SoADownedSystem>().slainValkyrie)
            {
                if (Main.rand.NextFloat() <= .5f)
                    NPC.position = player.position - new Vector2(500, 250);
                else NPC.position = player.position - new Vector2(-500, 250);
                //if (Player.GetModPlayer<SlayerPlayer>().slayerMode)
                //{
                //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("");
                //}
                //else
                //{
                //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("");
                //}
                NPC.localAI[0] = 1f;

            }
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;
            // Nothing will happen here
            if (attackCooldown <= 0)
            {
                attackType = Main.rand.Next(5);
                if (attackType == 2 || attackType == 4)
                {
                    attackType++;
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
                        // Feather Blade Spiral
                        attackTimer = 120;
                        attackCooldown = 60;
                        break;
                    case 3:
                        // Lance Dash
                        attackTimer = 120;
                        attackCooldown = 60;
                        break;
                    case 4:
                        // Storm Cloud
                        attackTimer = 600;
                        attackCooldown = 60;
                        break;
                    case 5:
                        // Sword Dance
                        attackTimer = 9 * 60;
                        attackCooldown = 60;
                        break;
                }
            }

            if (attackTimer > 0)
            {
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
                        if (attackTimer <= 60 && attackTimer > 2)
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Zero, ModContent.ProjectileType<ElectricTrail>(), 18, 0f, Main.myPlayer);
                        break;
                    case 1:
                        // Feather Blade Barrage
                        if (attackTimer == 240)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                        }
                        // + pattern
                        if (attackTimer == 180)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 projPos = targetPosition + new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos, Vector2.Normalize(targetPosition - projPos) * 7, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, 0, 60f);
                            }
                        }
                        if (attackTimer == 120)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                        }
                        // X pattern
                        if (attackTimer == 60)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 projPos = targetPosition + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos, Vector2.Normalize(targetPosition - projPos) * 7, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, 0, 60f);
                            }
                        }
                        break;
                    case 2:
                        // Feather Blade Spiral

                        break;
                    case 3:
                        // Lance Dash
                        if (attackTimer > 30)
                        {
                            NPC.velocity = Vector2.Normalize(player.Center + new Vector2(250 * (NPC.Center.X > player.Center.X ? 1 : -1), 0) - NPC.Center) * 12;
                        }
                        if (attackTimer == 30)
                        {
                            NPC.velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y - player.velocity.Y) - center) * 16;
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), center, NPC.velocity * 0.75f, ModContent.ProjectileType<StormLance>(), 16, 0, Main.myPlayer);
                        }
                        break;
                    case 4: 
                        // Storm Cloud

                        break;
                    case 5:
                        // Sword Dance
                        if (attackTimer > 8 * 60)
                        {
                            NPC.velocity = Vector2.Normalize((player.Center + new Vector2(500 * (NPC.Center.X > player.Center.X ? 1 : -1), -200)) - NPC.Center) * 12;
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
    }
}
