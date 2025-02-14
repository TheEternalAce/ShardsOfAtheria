using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged.EventHorizon
{
    public class BlackHoleBolt : ModProjectile
    {
        Point point;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(9);
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
            Projectile.alpha = 255;

            DrawOffsetX = 1;
        }

        public override void AI()
        {
            if (Projectile.alpha > 0) Projectile.alpha -= 25;
            else if (Projectile.alpha < 0) Projectile.alpha = 0;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                if (Projectile.ai[0] == 0f)
                {
                    point = new((int)Projectile.ai[1], (int)Projectile.ai[2]);
                    //Projectile.velocity *= 0.9f;
                    Projectile.ai[0] = 1f;
                }
                if (Projectile.ai[0] == 1f && player.ownedProjectileCounts[ModContent.ProjectileType<BlackHole>()] < 10)
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
                                if (Projectile.Distance(projectile.Center) < 20)
                                {
                                    Projectile.Kill();
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            var player = Projectile.GetPlayerOwner();
            if (Projectile.ai[0] == 1 && player.ownedProjectileCounts[ModContent.ProjectileType<BlackHole>()] < 10)
            {
                if (!player.IsLocal()) return;
                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                    Vector2.Zero, ModContent.ProjectileType<BlackHole>(), 180, 0, Projectile.owner);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var color = new Color(90, 10, 120);
            Projectile.DrawBloomTrail(color.UseA(50), SoA.DiamondBloom, MathHelper.PiOver2);
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
