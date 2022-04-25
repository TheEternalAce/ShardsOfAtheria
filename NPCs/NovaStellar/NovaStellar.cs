using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.ItemDropRules.Conditions;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles.NPCProj;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.NovaStellar
{
    [AutoloadBossHead]
    public class NovaStellar : ModNPC
    {
        public int dialogueTimer;

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
            DisplayName.SetDefault("Nova Stellar, the Harpy Knight");
            Main.npcFrameCount[NPC.type] = 1;

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
            Music = MusicID.Boss1;
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
			
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<ValkyrieCrown>()));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.GoldBar, 1, 10, 20));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.Feather, 1, 2, 5));
            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieSoulCrystal>()));
            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieStormLance>()));

            // Finally add the leading rule
            npcLoot.Add(notExpertRule);
            npcLoot.Add(slayerMode);
        }

        public override void OnKill()
        {
            dialogueTimer = 0;
            NPC.SetEventFlagCleared(ref SoAWorld.downedValkyrie, -1);
            //if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            //    Main.NewText("...");
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode || SecondStage)
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }

        public override bool PreAI()
        {
            if (ModContent.GetInstance<SoAWorld>().slainValkyrie)
            {
                Main.NewText("Nova Stellar, the Harpy Knight was slain...");
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

            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }

            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !ModContent.GetInstance<SoAWorld>().slainValkyrie)
            {
                if (Main.rand.NextFloat() <= .5f)
                    NPC.position = player.position - new Vector2(500, 250);
                else NPC.position = player.position - new Vector2(-500, 250);
                SoundEngine.PlaySound(SoundID.Roar, NPC.position, 0);
                //if (ModContent.GetInstance<SoAWorld>().slayerMode)
                //{
                //    Main.NewText("Alright, now- That look in your eyes... I must take you down here and now!");
                //}
                //else
                //{
                //    Main.NewText("Alright, now- Hey, that's my crest! How did you get that!?");
                //}
                NPC.localAI[0] = 1f;

            }
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;
            CheckSecondStage();
            if (NPC.life > 1)
            {
                if (SecondStage)
                {
                    DoSecondStage(player);
                }
                else
                {
                    DoFirstStage(player);
                }
            }
            // death drama
            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f; // increase our death timer.
                                 //NPC.velocity = Vector2.UnitY * NPC.velocity.Length();
                NPC.velocity = Vector2.Zero;
                //if (NPC.ai[3] > 1f && NPC.ai[3] == 2)
                //{
                //    if (ModContent.GetInstance<SoAWorld>().slayerMode)
                //        Main.NewText("*cough* *cough* You're... really strong huh..? Or am I weak..? Haha... Mother... I've failed... you...");
                //    else Main.NewText("*pant* *pant* You defeated me..?");
                //}
                if (NPC.ai[3] >= 120f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                }
                return;
            }
        }

        public void CheckSecondStage()
        {
            if (SecondStage)
            {
                // No point checking if the NPC is already in its second stage
                return;
            }

            //Run code when life is half of max or when the world is in Slayer mode
            if (NPC.life <= NPC.lifeMax/2 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity = Vector2.Zero;
                //Main.NewText("Okay, you're stronger than I thought, but I have an ace up my sleeve.");
                SecondStage = true;
                NPC.netUpdate = true;
            }
        }

        public void DoFirstStage(Player player)
        {
            FirstStageTimer++;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 position = NPC.Center;
                Vector2 targetPosition = Main.player[NPC.target].Center;
                Vector2 toTarget = targetPosition - position;

                toTarget.Normalize();

                //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles

                //Feather blade barrage
                if (FirstStageTimer == 120)
                    Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), position, toTarget * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (FirstStageTimer == 130)
                    Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), position, toTarget * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (FirstStageTimer == 140)
                    Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), position, toTarget * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (FirstStageTimer == 150)
                    Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), position, toTarget * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (FirstStageTimer == 160)
                    Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), position, toTarget * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);

                //Dash
                if (FirstStageTimer == 220)
                {
                    NPC.velocity = Vector2.Normalize(toTarget) * 10;
                    FirstStageTimer = 0;
                }
            }
        }

        public void DoSecondStage(Player player)
        {
            SecondStageTimer++;

            Vector2 position = NPC.Center;
            Vector2 targetPosition = Main.player[NPC.target].Center;
            Vector2 toTarget = targetPosition - position;
            Vector2 above = targetPosition + new Vector2(0, -300f);

            toTarget.Normalize();

            //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles

            //Feather blade barrage
            if (SecondStageTimer == 120)
            {
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), targetPosition + new Vector2(150, 0), Vector2.Normalize(targetPosition - (targetPosition + new Vector2(150, 0))) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), targetPosition + new Vector2(-150, 0), Vector2.Normalize(targetPosition - (targetPosition + new Vector2(-150, 0))) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), targetPosition + new Vector2(0, 150), Vector2.Normalize(targetPosition - (targetPosition + new Vector2(0, 150))) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), targetPosition + new Vector2(0, -150), Vector2.Normalize(targetPosition - (targetPosition + new Vector2(0, -150))) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
            }
            if (SecondStageTimer == 180)
            {
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), position, toTarget * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
            }
            if (SecondStageTimer == 240)
            {
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), targetPosition + new Vector2(125, 125), Vector2.Normalize(targetPosition - (targetPosition + new Vector2(125, 125))) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), targetPosition + new Vector2(150, -125), Vector2.Normalize(targetPosition - (targetPosition + new Vector2(150, -125))) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), targetPosition + new Vector2(-125, 125), Vector2.Normalize(targetPosition - (targetPosition + new Vector2(-125, 125))) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), targetPosition + new Vector2(-125, -150), Vector2.Normalize(targetPosition - (targetPosition + new Vector2(-125, -150))) * 7f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
            }

            //Lightning strike
            if (SecondStageTimer == 300)
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), above, Vector2.Zero, ModContent.ProjectileType<LightningBoltSpawner>(), 18, 0f, Main.myPlayer);

            //Dash
            if (SecondStageTimer == 320)
            {
                NPC.velocity = Vector2.Normalize(toTarget) * 10;
            }
            if (SecondStageTimer >= 320)
                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), position, Vector2.Zero, ModContent.ProjectileType<ElectricTrail>(), 18, 0f, Main.myPlayer);

            if (SecondStageTimer > 380)
                SecondStageTimer = 0;
        }
    }
}