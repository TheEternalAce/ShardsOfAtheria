using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs.NovaSkyloft
{
    [AutoloadBossHead]
    public class HarpyKnight : ModNPC
    {
        public override string HeadTexture => "SagesMania/NPCs/NovaSkyloft/HarpyKnight_Head_Boss";
        private int attackTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Skyloft, the Harpy Knight");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 86;
            npc.height = 60;
            npc.damage = 29;
            npc.defense = 8;
            npc.lifeMax = 1000;
            npc.HitSound = SoundID.NPCHit1;
            npc.knockBackResist = 0f;
            npc.aiStyle = 14;
            npc.boss = true;
            npc.noGravity = true;
            music = MusicID.Boss1;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.npcSlots = 15f;
        }

        public override bool CheckDead()
        {
            if (npc.ai[3] == 0f)
            {
                npc.ai[3] = 1f;
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1f);
        }

        public override bool PreAI()
        {
            Player player = Main.LocalPlayer;
            if (ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                Main.NewText("Nova Skyloft, the Harpy Knight was slain...");
                npc.active = false;
            }
            if (player.dead)
                npc.active = false;
            return base.PreAI();
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;

            if (npc.localAI[0] == 0f && npc.life >= 1 && !ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                if (Main.rand.NextFloat() <= .5f)
                    npc.position = player.position - new Vector2(500, 250);
                else npc.position = player.position - new Vector2(-500, 250);
                Main.PlaySound(SoundID.Roar, npc.position, 0);
                if (ModContent.GetInstance<SMWorld>().slayerMode)
                {
                    Main.NewText("That look in your eyes... I must take you down here and now!");
                }
                else
                {
                    if (player.ZoneOverworldHeight)
                        Main.NewText("So you bring me down to the surface? Well then, have at you!");
                    else if (player.ZoneSkyHeight)
                        Main.NewText("Now that you're here, let's do this!");
                }
                npc.localAI[0] = 1f;

            }
            npc.TargetClosest();
            if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && !npc.dontTakeDamage)
            {
                Vector2 position = npc.Center;
                Vector2 targetPosition = Main.player[npc.target].Center;
                Vector2 direction = targetPosition - position;
                Vector2 above = player.Center + new Vector2(0, -300f);

                Vector2 perturbedDirection = direction + new Vector2().RotatedByRandom(MathHelper.ToRadians(5));

                direction.Normalize();
                attackTimer++;
                //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles
                //Feather blade barrage
                if (attackTimer == 120)
                    Projectile.NewProjectile(position, direction * 10f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (attackTimer == 130)
                    Projectile.NewProjectile(position, direction * 10f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (attackTimer == 140)
                    Projectile.NewProjectile(position, direction * 10f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (attackTimer == 150)
                    Projectile.NewProjectile(position, direction * 10f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                if (attackTimer == 160)
                    Projectile.NewProjectile(position, direction * 10f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                
                if (attackTimer == 220)
                {
                    npc.velocity = Vector2.Normalize(player.Center - npc.Center) * 10;
                    attackTimer = 0;
                }
            }
            npc.spriteDirection = player.Center.X > npc.Center.X ? 1 : -1;

            if (npc.position.Y >= player.position.Y + 300)
                npc.position.Y = player.position.Y + 300;

            // death drama
            if (npc.ai[3] > 0f)
            {
                npc.dontTakeDamage = true;
                npc.ai[3] += 1f; // increase our death timer.
                                 //npc.velocity = Vector2.UnitY * npc.velocity.Length();
                npc.velocity.X = 0f;
                npc.velocity.Y = 0;
                if (npc.ai[3] > 1f && npc.ai[3] == 2)
                {
                    Main.NewText("Okay, you're stronger than I thought, but I have an ace up my sleeve.");
                }
                if (npc.ai[3] >= 120f)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                    Vector2 spawnAt = npc.Center;
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

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}