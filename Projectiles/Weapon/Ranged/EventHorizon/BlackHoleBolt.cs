using BattleNetworkElements.Utilities;
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
            Projectile.AddAqua();
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
                    point = (player.MountedCenter + Projectile.velocity + (Projectile.rotation - MathHelper.ToRadians(90)).ToRotationVector2().SafeNormalize(Vector2.Zero) * Vector2.Distance(player.Center, Main.MouseWorld)).ToPoint();
                    Projectile.velocity *= 0.9f;
                    Projectile.ai[0] = 1f;
                }
                if (Projectile.ai[0] == 1f)
                {
                    Dust dust = Dust.NewDustPerfect(point.ToVector2(), DustID.Stone, Vector2.Zero, 0, new Color(90, 10, 120));
                    dust.noGravity = true;
                    dust.fadeIn = 1;
                    if (Projectile.Hitbox.Contains(point))
                    {
                        Projectile.Kill();
                    }
                }
                if (Projectile.ai[0] == 2f)
                {
                    foreach (var projectile in Main.projectile)
                    {
                        if (projectile.type == ModContent.ProjectileType<BlackHole>())
                        {
                            if (projectile.owner == Projectile.owner)
                            {
                                if (Projectile.Hitbox.Contains(projectile.Center.ToPoint()))
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
            Projectile.DrawProjectilePrims(color, ShardsProjectileHelper.DiamondX1);
            return base.PreDraw(ref lightColor);
        }
    }
}
