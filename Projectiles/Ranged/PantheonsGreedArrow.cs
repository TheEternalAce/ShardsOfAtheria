using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class PantheonsGreedArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.timeLeft = 60 * 5;
            Projectile.aiStyle = -1;
            Projectile.arrow = true;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;

            DrawOffsetX = -2;
        }

        int gravityTimer = 16;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Projectile.ApplyGravity(ref gravityTimer, 0.5f, 32f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GoldCoin, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawAfterImage(lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}