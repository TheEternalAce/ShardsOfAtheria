using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SagesMania.Items.Accessories;
using SagesMania.Items.Placeable;
using SagesMania.Items.SlayerItems;
using SagesMania.Items.Weapons.Magic;
using SagesMania.Items.Weapons.Melee;
using SagesMania.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.NPCs.NovaSkyloft
{
    [AutoloadBossHead]
    public class ValkyrieNova : ModNPC
    {
        public override string Texture => "SagesMania/NPCs/NovaSkyloft/HarpyKnight";
        public override string HeadTexture => "SagesMania/NPCs/NovaSkyloft/HarpyKnight_Head_Boss";

        private int attackTimer;
        private int movementTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Skyloft, the Lightning Valkyrie");
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
            npc.DeathSound = SoundID.NPCDeath1;
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
            if (player.dead)
                npc.active = false;
            if (ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                Main.NewText("Nova Skyloft, the Harpy Knight was slain...");
                npc.active = false;
            }
            return base.PreAI();
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(npc.position, npc.height, npc.width, DustID.Electric,
                    npc.velocity.X * .2f, npc.velocity.Y * .2f, 200, Scale: 1f);
                dust.velocity += npc.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }

            if (npc.localAI[0] == 0f && npc.life >= 1 && !ModContent.GetInstance<SMWorld>().slainValkyrie)
            {
                Main.NewText("Behold, my Valkyrie form!");
                npc.localAI[0] = 1f;
            }
            npc.TargetClosest();
            if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && !npc.dontTakeDamage)
            {
                Vector2 position = npc.Center;
                Vector2 targetPosition = Main.player[npc.target].Center;
                Vector2 direction = targetPosition - position;
                Vector2 above = targetPosition + new Vector2(0, -300f);

                Vector2 perturbedDirection = direction + new Vector2().RotatedByRandom(MathHelper.ToRadians(5));

                direction.Normalize();
                attackTimer++;
                //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles
                //Feather blade barrage
                if (attackTimer == 120)
                {
                    Projectile.NewProjectile(targetPosition + new Vector2(150, 0), new Vector2(-3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, new Vector2(-10, 0).ToRotation(), Main.rand.Next(100));
                    Projectile.NewProjectile(targetPosition + new Vector2(-150, 0), new Vector2(3, 0), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, new Vector2(10, 0).ToRotation(), Main.rand.Next(100));
                    Projectile.NewProjectile(targetPosition + new Vector2(0, 150), new Vector2(0, -3), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, new Vector2(0, -10).ToRotation(), Main.rand.Next(100));
                    Projectile.NewProjectile(targetPosition + new Vector2(0, -150), new Vector2(0, 3), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, new Vector2(0, 10).ToRotation(), Main.rand.Next(100));
                }
                if (attackTimer == 140)
                {
                    Projectile.NewProjectile(position, direction * 10f, ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer);
                }
                if (attackTimer == 160)
                {
                    Projectile.NewProjectile(targetPosition + new Vector2(125, 125), new Vector2(-3, -3), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, new Vector2(-10, -10).ToRotation());
                    Projectile.NewProjectile(targetPosition + new Vector2(125, -125), new Vector2(-3, 3), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, new Vector2(-10, 10).ToRotation());
                    Projectile.NewProjectile(targetPosition + new Vector2(-125, 125), new Vector2(3, -3), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, new Vector2(10, -10).ToRotation());
                    Projectile.NewProjectile(targetPosition + new Vector2(-125, -125), new Vector2(3, 3), ModContent.ProjectileType<FeatherBlade>(), 18, 0f, Main.myPlayer, new Vector2(10, 10).ToRotation());
                }
                
                //Lightning strikes
                if (attackTimer == 220)
                    Projectile.NewProjectile(above, direction * 10f, ProjectileID.VortexLightning, 18, 0f, Main.myPlayer, new Vector2(0, 10).ToRotation(), Main.rand.Next(100));
                if (attackTimer == 280)
                    npc.velocity = Vector2.Normalize(direction) * 10;
                if (attackTimer == 340)
                    attackTimer = 0;
            }
            npc.spriteDirection = Main.player[npc.target].Center.X > npc.Center.X ? 1 : -1;

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
                    if (!ModContent.GetInstance<SMWorld>().slayerMode)
                    {
                        Main.NewText("*pant* *pant* You defeated me..? Darn, oh well, that was fun regardless!");
                    }
                    else
                    {
                        Main.NewText("*cough* *cough* You're... really strong huh..? Or am I weak..? Haha... Mom... I failed...");
                    }
                }
                if (npc.ai[3] >= 120f)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                }
                return;
            }
        }

        public override void NPCLoot()
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(ModContent.ItemType<ValkyrieCrown>());
            dropChooser.Add(ModContent.ItemType<ValkyrieBlade>());
            int choice = dropChooser;

            if (!ModContent.GetInstance<SMWorld>().downedValkyrie)
                ModContent.GetInstance<SMWorld>().downedValkyrie = true;
            if (!ModContent.GetInstance<SMWorld>().slayerMode)
                Main.NewText("I shall take my leave now, in the meantime, here's your reward.");
            if (!ModContent.GetInstance<SMWorld>().slayerMode)
            {
                if (Main.expertMode)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<NovaBossBag>());
                else
                {
                    Item.NewItem(npc.getRect(), ItemID.Feather, Main.rand.Next(10, 19));
                    Item.NewItem(npc.getRect(), ItemID.GoldBar, Main.rand.Next(10, 19));
                    Item.NewItem(npc.getRect(), choice);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), Main.rand.Next(5, 7));
                }
            }
            else
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<GildedValkyrieWings>());
                Item.NewItem(npc.getRect(), ModContent.ItemType<ValkyrieCrown>());
                Item.NewItem(npc.getRect(), ModContent.ItemType<ValkyrieBlade>());
                Item.NewItem(npc.getRect(), ItemID.Feather, 4000);
                Item.NewItem(npc.getRect(), ItemID.GoldBar, 4000);
                Item.NewItem(npc.getRect(), ModContent.ItemType<ValkyrieStormLance>());
                Item.NewItem(npc.getRect(), ModContent.ItemType<PhaseOreItem>(), 20);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 60);
        }
    }
}