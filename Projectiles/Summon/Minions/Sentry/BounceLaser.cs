using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Sentry
{
    public class BounceLaser : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 40;
            ProjectileID.Sets.SentryShot[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 20;
            Projectile.penetrate = 10;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            foreach (var projectile in Main.projectile)
            {
                if (projectile.type == ModContent.ProjectileType<MirrorPrism>() && projectile.active &&
                    Projectile.Hitbox.Intersects(projectile.Hitbox))
                {
                    projectile.Kill();
                    var prism = ShardsHelpers.FindClosestProjectile(Projectile.Center, 1000, (proj) => CheckProjectile(proj, Projectile.owner));
                    var npc = Projectile.FindClosestNPC(null, 1000);
                    var vector = Vector2.Zero;
                    if (npc != null)
                    {
                        vector = npc.Center - projectile.Center;
                    }
                    if (prism != null && Projectile.ai[0] < 6)
                    {
                        vector = prism.Center - projectile.Center;
                        Projectile.ai[0]++;
                    }
                    vector.Normalize();
                    Projectile.velocity = vector * 8f;
                    break;
                }
            }
        }

        private static bool CheckProjectile(Projectile projectile, int owner)
        {
            if (projectile.type != ModContent.ProjectileType<MirrorPrism>()) return false;
            if (projectile.owner != owner) return false;
            if (Math.Abs(projectile.velocity.X) > 1) return false;
            if (Math.Abs(projectile.velocity.Y) > 1) return false;
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBlurTrail(SoA.ElectricColor, SoA.LineBlur);
            return base.PreDraw(ref lightColor);
        }
    }
}
