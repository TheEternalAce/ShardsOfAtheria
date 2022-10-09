using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class StormCloud : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Blank";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Cloud");
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

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {
                Projectile.Center = player.Center + new Vector2(0, -400);
                Projectile.ai[0] = 1;
            }

            Rectangle saferArea = new()
            {
                Width = Projectile.width,
                Height = Projectile.height * 4,
                Location = Projectile.position.ToPoint()
            };

            Dust dustLeft = Dust.NewDustDirect(Projectile.position, 1, Projectile.height * 4, DustID.Electric);
            dustLeft.noGravity = true;
            Dust dustRight = Dust.NewDustDirect(new Vector2(Projectile.position.X + Projectile.width, Projectile.position.Y), 1, Projectile.height * 4, DustID.Electric);
            dustRight.noGravity = true;
            Dust dustBottom = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y + (Projectile.height * 4)), Projectile.width, 1, DustID.Electric);
            dustBottom.noGravity = true;

            if (!saferArea.Intersects(player.getRect()) && Projectile.ai[0] == 1)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.Center + new Vector2(0, -200), Vector2.Zero, ModContent.ProjectileType<StormCloudPunisher>(), 50, 0, Main.myPlayer);
                Projectile.ai[0] = 2;
            }
            else if (saferArea.Intersects(player.getRect()))
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.owner == Main.myPlayer && proj.type == ModContent.ProjectileType<StormCloudPunisher>())
                    {
                        proj.Kill();
                    }
                }
                Projectile.ai[0] = 1;
            }

            if (++Projectile.ai[1] == 20 && Projectile.timeLeft > 20)
            {
                for (int i = 0; i < 12; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position + new Vector2(Main.rand.Next(Projectile.width), 100), Vector2.Zero, ModContent.ProjectileType<LightningBoltSpawner>(), 10, 0, Main.myPlayer);
                }
            }
            if (Projectile.ai[1] >= 60)
            {
                Projectile.ai[1] = 0;
            }

            for (int i = 0; i < 50; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.RainCloud, Scale: 2f);
                dust.noGravity = true;
            }

            if (Projectile.timeLeft == 20)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.type == ModContent.ProjectileType<StormCloudPunisher>())
                    {
                        proj.Kill();
                    }
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}
