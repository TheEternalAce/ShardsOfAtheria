using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class DecaBladeProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = false;
            projectile.timeLeft = 5 * 60;
            projectile.extraUpdates = 2;

            drawOffsetX = -26;
            drawOriginOffsetX = 13;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
        }
    }
}
