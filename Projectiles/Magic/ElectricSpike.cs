using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class ElectricSpike : ModProjectile
    {
        public int flightTimer;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;

            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
            Projectile.penetrate = 5;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 8;

            DrawOffsetX = -4;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            flightTimer++;
            Vector2 destination = new(Projectile.ai[0], Projectile.ai[1]);
            if (flightTimer > 20)
            {
                if (Projectile.Distance(destination) < 50 && Projectile.ai[2] == 0)
                {
                    Projectile.ai[2] = 1;
                }
                if (Projectile.ai[2] == 0)
                {
                    Projectile.friendly = true;
                    Projectile.Track(destination, 20f, 20f);
                }
            }
            if (Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, 0, 0, 200, Scale: 1f);
                dust.noGravity = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
