using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Buffs.AnyDebuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class StormCloud : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
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

            Vector2 newCenter = player.Center;
            int dirX = saferArea.Center.X < player.Center.X ? 1 : -1;
            float distX = MathHelper.Distance(player.Center.X, saferArea.Center.X);
            float distXMax = saferArea.Width / 2;
            if (distX > distXMax)
            {
                newCenter.X -= (distX - distXMax) * dirX;
            }
            int dirY = saferArea.Center.Y < player.Center.Y ? 1 : -1;
            float distY = MathHelper.Distance(player.Center.Y, saferArea.Center.Y);
            float distYMax = saferArea.Height / 2;
            if (distY > distYMax)
            {
                newCenter.Y -= (distY - distYMax) * dirY;
            }
            player.Center = newCenter;

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

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
