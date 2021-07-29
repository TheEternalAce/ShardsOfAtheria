using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class DecaSwarmer : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 40;
            projectile.extraUpdates = 1;
            projectile.penetrate = -1;

            drawOffsetX = -12;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }
    }
}
