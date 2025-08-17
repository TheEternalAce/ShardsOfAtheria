using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.NPCProj.Elizabeth;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class SilverBoltFriendly : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<SilverBoltHostile>().Texture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

            SoAGlobalProjectile.Metalic.Add(Type, 1f);

            Projectile.AddDamageType(7);
            Projectile.AddRedemptionElement(1);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.timeLeft = 2 * 60;
            Projectile.aiStyle = 0;
            Projectile.arrow = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Ranged;

            DrawOffsetX = -2;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
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
