using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class EntropicCut_Red : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            Projectile.AddDamageType(2, 11);
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.tileCollide = false;
            Projectile.localNPCHitCooldown = 20;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.extraUpdates = 1;

            Projectile.timeLeft = 15;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var color = Color.Red;
            lightColor = Color.White;
            Projectile.DrawBloomTrail(color.UseA(50), SoA.DiamondBloom, MathHelper.PiOver2);
            Projectile.DrawAfterImage(lightColor);
            return false;
        }
    }

    public class EntropicCut_Blue : EntropicCut_Red
    {
        public override bool PreDraw(ref Color lightColor)
        {
            var color = Color.Blue;
            lightColor = Color.White;
            Projectile.DrawBloomTrail(color.UseA(50), SoA.DiamondBloom, MathHelper.PiOver2);
            Projectile.DrawAfterImage(lightColor);
            return false;
        }
    }
}
