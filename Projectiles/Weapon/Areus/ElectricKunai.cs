using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus
{
    public class ElectricKunai : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.scale = .75f;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 600;

            DrawOffsetX = -28;
            DrawOriginOffsetX = 14;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 20f)
            {
                Projectile.ai[0] = 15f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if (Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, 0, 0, 200, Scale: 1f);
                dust.noGravity = true;
            }
        }
    }
}
