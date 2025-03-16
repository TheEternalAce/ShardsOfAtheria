using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class HardlightBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddDamageType(7);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 180;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width,
                    Projectile.height, ModContent.DustType<HardlightDust_Blue>(),
                    Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                dust.noGravity = true;
                var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width,
                    Projectile.height, ModContent.DustType<HardlightDust_Pink>(),
                    Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                dust1.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBloomTrail(SoA.HardlightColor.UseA(50), SoA.DiamondBloom, MathHelper.PiOver2);
            lightColor = Color.White;
            return true;
        }
    }
}
