﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class StormCloud : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(12);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 1300;
            Projectile.height = 200;

            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 220;
        }

        Vector2[] spawnPoints = new Vector2[8];
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {
                Projectile.Center = player.Center + new Vector2(0, -400);
                Projectile.ai[0] = 1;
            }

            if (++Projectile.ai[1] == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    spawnPoints[i] = Projectile.position + new Vector2(Main.rand.Next(Projectile.width), 180);
                }
            }

            if (!SoA.Eternity())
            {
                for (int i = 0; i < 8; i++)
                {
                    if (Projectile.timeLeft > 60)
                    {
                        if (spawnPoints != null)
                        {
                            Dust.NewDustPerfect(spawnPoints[i], DustID.Electric);
                        }
                    }
                }
            }

            Rectangle saferArea = new()
            {
                Width = Projectile.width,
                Height = Projectile.height * 4,
                Location = Projectile.position.ToPoint()
            };

            Vector2 newCenter = player.Center;
            int dirX = saferArea.Center.X < player.Center.X ? 1 : -1;
            float distX = MathHelper.Distance(player.Center.X, saferArea.Center.X);
            float distXMax = saferArea.Width / 2;
            if (distX > distXMax)
            {
                newCenter.X -= (distX - distXMax) * dirX;
                player.velocity.X = 0;
                player.AddBuff<ElectricShock>(120);
            }
            int dirY = saferArea.Center.Y < player.Center.Y ? 1 : -1;
            float distY = MathHelper.Distance(player.Center.Y, saferArea.Center.Y);
            float distYMax = saferArea.Height / 2;
            if (distY > distYMax)
            {
                newCenter.Y -= (distY - distYMax) * dirY;
                player.velocity.Y = 0;
                player.AddBuff<ElectricShock>(120);
            }
            player.Center = newCenter;
            if (Projectile.ai[1] >= 60)
            {
                SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
                Projectile.ai[1] = 0;
                Vector2 velocity = new(0, 2);
                for (int i = 0; i < 8; i++)
                {
                    var proj = Projectile.NewProjectile(Projectile.GetSource_FromAI(), spawnPoints[i], velocity,
                        ModContent.ProjectileType<LightningBolt>(), 18, 0, Main.myPlayer);
                    Main.projectile[proj].tileCollide = false;
                }
            }

            for (int i = 0; i < 50; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.RainCloud, Scale: 2f);
                dust.noGravity = true;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<ElectricShock>(600);
            if (SoA.Eternity() && Main.rand.NextBool(5))
            {
                target.AddBuff(ModContent.Find<ModBuff>("FargowiltasSouls", "ClippedWingsBuff").Type, 600);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
