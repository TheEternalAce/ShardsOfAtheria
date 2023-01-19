using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Minions
{
    public class IceFragment : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 1;

            DrawOffsetX = 6;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }
    }
}
