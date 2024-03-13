using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.PlagueRail
{
    public class PlagueBullet : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 5;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            int dustDelay = 11;
            if (++Projectile.ai[0] >= dustDelay)
            {
                if (!Projectile.hostile)
                {
                    Projectile.hostile = true;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.PaleGreen;
            Projectile.DrawBlurTrail(lightColor, SoA.LineBlur);
            return base.PreDraw(ref lightColor);
        }
    }
}
