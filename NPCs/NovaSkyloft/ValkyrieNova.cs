using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.NovaSkyloft
{
    [AutoloadBossHead]
    public class ValkyrieNova : ModNPC
    {
        public override string Texture => "ShardsOfAtheria/NPCs/NovaSkyloft/HarpyKnight";
        public override string HeadTexture => "ShardsOfAtheria/NPCs/NovaSkyloft/HarpyKnight_Head_Boss";

        private int attackTimer;
        private int dialogueTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Skyloft, the Lightning Valkyrie");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 86;
            NPC.height = 60;
            NPC.damage = 29;
            NPC.defense = 8;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.boss = true;
            NPC.noGravity = true;
            Music = MusicID.Boss2;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.npcSlots = 15f;
        }

        public override bool CheckDead()
        {
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 1.5f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 1f);
        }

        public override bool PreAI()
        {
            Player player = Main.LocalPlayer;
            if (player.dead)
                NPC.active = false;
            if (ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                Main.NewText("Nova Skyloft, the Harpy Knight was slain...");
                NPC.active = false;
            }
            return base.PreAI();
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.height, NPC.width, DustID.Electric,
                    NPC.velocity.X * .2f, NPC.velocity.Y * .2f, 200, Scale: 1f);
                dust.velocity += NPC.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }

            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                Main.NewText("Behold, my Valkyrie form! (...Shut up the sprite artist is lazy)");
                NPC.localAI[0] = 1f;
            }
            NPC.TargetClosest();
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && !NPC.dontTakeDamage)
            {
                Vector2 position = NPC.Center;
                Vector2 targetPosition = Main.player[NPC.target].Center;
                Vector2 direction = targetPosition - position;
                Vector2 above = targetPosition + new Vector2(0, -300f);

                Vector2 perturbedDirection = direction + new Vector2().RotatedByRandom(MathHelper.ToRadians(5));

                direction.Normalize();
                attackTimer++;
                //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles
                //Feather blade barrage
                if (attackTimer == 120)
                {
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), targetPosition + new Vector2(150, 0), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), targetPosition + new Vector2(-150, 0), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), targetPosition + new Vector2(0, 150), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), targetPosition + new Vector2(0, -150), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                }
                if (attackTimer == 140)
                {
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), position, direction * 10f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                }
                if (attackTimer == 160)
                {
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), targetPosition + new Vector2(125, 125), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), targetPosition + new Vector2(150, -125), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), targetPosition + new Vector2(-125, 125), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), targetPosition + new Vector2(-125, -150), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                }
                
                //Lightning strikes
                if (attackTimer == 220)
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), above, direction * 10f, ProjectileID.VortexLightning, 18, 0f, Main.myPlayer, new Vector2(0, 10).ToRotation(), Main.rand.Next(100));
                if (attackTimer == 280)
                    NPC.velocity = Vector2.Normalize(direction) * 10;
                if (attackTimer == 340)
                    attackTimer = 0;
            }
            NPC.spriteDirection = Main.player[NPC.target].Center.X > NPC.Center.X ? 1 : -1;

            if (NPC.position.Y >= player.position.Y + 300)
                NPC.position.Y = player.position.Y + 300;

            // death drama
            if (NPC.ai[3] > 0f)
            {
                NPC.dontTakeDamage = true;
                NPC.ai[3] += 1f; // increase our death timer.
                                 //NPC.velocity = Vector2.UnitY * NPC.velocity.Length();
                NPC.velocity.X = 0f;
                NPC.velocity.Y = 0;
                if (NPC.ai[3] > 1f && NPC.ai[3] == 2)
                {
                    if (!ModContent.GetInstance<SMWorld>().slayerMode)
                    {
                        dialogueTimer++;
                        if (dialogueTimer == 0)
                            Main.NewText("*pant* *pant* You defeated me..?");
                        if (dialogueTimer == 20)
                            Main.NewText("...");
                    }
                    else
                    {
                        Main.NewText("*cough* *cough* You're... really strong huh..? Or am I weak..? Haha... Mother... I've failed... you...");
                    }
                }
                if (NPC.ai[3] >= 120f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                }
                return;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(ModContent.ItemType<ValkyrieCrown>());
            dropChooser.Add(ModContent.ItemType<ValkyrieBlade>());
            int choice = dropChooser;
            dialogueTimer = 0;

            if (!ModContent.GetInstance<SMWorld>().downedValkyrie)
                ModContent.GetInstance<SMWorld>().downedValkyrie = true;
            if (!ModContent.GetInstance<SMWorld>().slayerMode)
            {
                Main.NewText("I'll take back my crest now, in the meantime, here's your reward.");
                if (Main.expertMode)
                    Item.NewItem(NPC.getRect(), ModContent.ItemType<NovaBossBag>());
                else
                {
                    Item.NewItem(NPC.getRect(), ItemID.Feather, Main.rand.Next(10, 19));
                    Item.NewItem(NPC.getRect(), ItemID.GoldBar, Main.rand.Next(10, 19));
                    Item.NewItem(NPC.getRect(), choice);
                    Item.NewItem(NPC.getRect(), ModContent.ItemType<PhaseOreItem>(), Main.rand.Next(5, 7));
                }
            }
            else
            {
                Item.NewItem(NPC.getRect(), ModContent.ItemType<GildedValkyrieWings>());
                Item.NewItem(NPC.getRect(), ModContent.ItemType<ValkyrieCrown>());
                Item.NewItem(NPC.getRect(), ModContent.ItemType<ValkyrieBlade>());
                Item.NewItem(NPC.getRect(), ItemID.Feather, 4000);
                Item.NewItem(NPC.getRect(), ItemID.GoldBar, 4000);
                Item.NewItem(NPC.getRect(), ModContent.ItemType<ValkyrieStormLance>());
                Item.NewItem(NPC.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 60);
        }
    }
}