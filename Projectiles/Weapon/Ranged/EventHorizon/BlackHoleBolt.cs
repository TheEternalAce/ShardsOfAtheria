using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.EventHorizon
{
    public class BlackHoleBolt : ModProjectile
    {
        Point point;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 2 * 60;
            Projectile.tileCollide = false;

            DrawOffsetX = 1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Main.myPlayer == Projectile.owner)
            {
                Player player = Main.player[Projectile.owner];

                if (Projectile.ai[0] == 0f)
                {
                    point = new((int)Projectile.ai[1], (int)Projectile.ai[2]);
                    //Projectile.velocity *= 0.9f;
                    Projectile.ai[0] = 1f;
                }
                if (Projectile.ai[0] == 1f)
                {
                    Dust dust = Dust.NewDustPerfect(point.ToVector2(), DustID.Stone, Vector2.Zero, 0, new Color(90, 10, 120));
                    dust.noGravity = true;
                    dust.fadeIn = 1;
                    if (Projectile.Distance(point.ToVector2()) <= 10)
                    {
                        Projectile.Kill();
                    }
                }
                if (Projectile.ai[0] == 2f)
                {
                    Projectile.alpha = 255;
                    Projectile.ai[0] = 3f;
                }
                if (Projectile.ai[0] == 3f)
                {
                    if (Projectile.alpha > 0)
                    {
                        Projectile.alpha -= 25;
                    }
                    if (Projectile.alpha < 0)
                    {
                        Projectile.alpha = 0;
                    }
                    foreach (var projectile in Main.projectile)
                    {
                        if (projectile.type == ModContent.ProjectileType<BlackHole>())
                        {
                            if (projectile.owner == Projectile.owner && projectile.active)
                            {
                                if (Projectile.Hitbox.Intersects(projectile.Hitbox))
                                {
                                    Projectile.Kill();
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (Projectile.ai[0] == 1)
            {
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                    Vector2.Zero, ModContent.ProjectileType<BlackHole>(), 180, 0, Projectile.owner);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var color = new Color(90, 10, 120);
            lightColor = Color.White;
            Projectile.DrawProjectilePrims(color, ShardsHelpers.DiamondX1);
            return base.PreDraw(ref lightColor);
        }
    }
}
