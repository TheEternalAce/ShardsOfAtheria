using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Globals;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus
{
    public class ElectricSpike : ModProjectile
    {
        public int flightTimer;
        Vector2 to;

        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.AreusProjectile.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;

            Projectile.aiStyle = 0;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.arrow = true;
            Projectile.light = 1;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;

            DrawOffsetX = 4;
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
                Projectile.tileCollide = true;
            if (Main.rand.NextBool(20))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            }
        }
    }
}
