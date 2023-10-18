using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.EnergyScythe
{
    public class EnergyWave : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 122;
            Projectile.height = 122;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 10;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;

            DrawOffsetX = -5;
            DrawOriginOffsetY = 3;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
