using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.NovaSkyloft
{
    [AutoloadBossHead]
    public class HarpyKnight : ModNPC
    {
        public override string HeadTexture => "ShardsOfAtheria/NPCs/NovaSkyloft/HarpyKnight_Head_Boss";
        private int attackTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Stellar, the Harpy Knight");
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
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.boss = true;
            NPC.noGravity = true;
            Music = MusicID.Boss1;
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
            if (ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                Main.NewText("Nova Stellar, the Harpy Knight was slain...");
                NPC.active = false;
            }
            if (player.dead)
                NPC.active = false;
            return base.PreAI();
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;

            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                if (Main.rand.NextFloat() <= .5f)
                    NPC.position = player.position - new Vector2(500, 250);
                else NPC.position = player.position - new Vector2(-500, 250);
                SoundEngine.PlaySound(SoundID.Roar, NPC.position, 0);
                if (ModContent.GetInstance<SMWorld>().slayerMode)
                {
                    Main.NewText("Alright, now- That look in your eyes... I must take you down here and now!");
                }
                else
                {
                    if (player.ZoneOverworldHeight)
                        Main.NewText("Alright, now- Hey, that's my crest! How did you get that!?");
                }
                NPC.localAI[0] = 1f;

            }
            NPC.TargetClosest();
            if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && !NPC.dontTakeDamage)
            {
                Vector2 position = NPC.Center;
                Vector2 targetPosition = Main.player[NPC.target].Center;
                Vector2 direction = targetPosition - position;
                Vector2 above = player.Center + new Vector2(0, -300f);

                Vector2 perturbedDirection = direction + new Vector2().RotatedByRandom(MathHelper.ToRadians(5));

                direction.Normalize();
                attackTimer++;
                //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles
                //Feather blade barrage
                if (attackTimer == 120)
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), position, direction, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (attackTimer == 130)
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), position, direction, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (attackTimer == 140)
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), position, direction, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (attackTimer == 150)
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), position, direction, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (attackTimer == 160)
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), position, direction, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);

                if (attackTimer == 220)
                {
                    NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 10;
                    attackTimer = 0;
                }
            }
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;

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
                    Main.NewText("Okay, you're stronger than I thought, but I have an ace up my sleeve.");
                }
                if (NPC.ai[3] >= 120f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                    Vector2 spawnAt = NPC.Center;
                    NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<ValkyrieNova>());
                }
                return;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
                target.AddBuff(BuffID.Electrified, 60);
        }
    }
}