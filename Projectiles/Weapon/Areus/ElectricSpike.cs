using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus
{
    public class ElectricSpike : ModProjectile
    {
        public int flightTimer;
        Vector2 to;

        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;

            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.light = 1;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 8;

            DrawOffsetX = -4;
        }

        public override void AI()
        {
            if (flightTimer == 0)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    to = Main.MouseWorld;
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            flightTimer++;
            if (flightTimer >= 20 && flightTimer < 60)
            {
                Projectile.friendly = true;
                Projectile.velocity += Vector2.Normalize(to - Projectile.Center);
                Projectile.netUpdate = true;
            }
            if (flightTimer == 50)
            {
                Projectile.velocity = Vector2.Normalize(to - Projectile.Center) * 16f;
                Projectile.netUpdate = true;
            }
            if (Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, 0, 0, 200, Scale: 1f);
                dust.noGravity = true;
            }
        }
    }
}
