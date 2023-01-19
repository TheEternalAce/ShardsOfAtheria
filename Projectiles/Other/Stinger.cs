using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class Stinger : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.t_Honey, 0f, 0f, 0, default(Color), 0.9f).noGravity = true;
            }
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                for (int num230 = 0; num230 < 20; num230++)
                {
                    Dust dust5 = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.t_Honey, 0f, 0f, 0, default(Color), 1.3f);
                    dust5.noGravity = true;
                    dust5.velocity += Projectile.velocity * 0.75f;
                }
                for (int num231 = 0; num231 < 10; num231++)
                {
                    Dust dust6 = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.t_Honey, 0f, 0f, 0, default(Color), 1.3f);
                    dust6.noGravity = true;
                    dust6.velocity *= 2f;
                }
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
        }
    }
}
