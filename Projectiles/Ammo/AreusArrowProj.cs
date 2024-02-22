using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ammo
{
    public class AreusArrowProj : ModProjectile
    {
        Point point;

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

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
                    SoundEngine.PlaySound(SoundID.Item91, Projectile.position);
                    point = (player.MountedCenter + Projectile.velocity + (Projectile.rotation - MathHelper.PiOver2).ToRotationVector2().SafeNormalize(Vector2.Zero) * Vector2.Distance(player.Center, Main.MouseWorld)).ToPoint();
                    Projectile.tileCollide = true;
                    Projectile.velocity *= 0.9f;
                    Projectile.ai[0] = 1f;
                }
                if (Projectile.ai[0] == 1f)
                {
                    Dust dust = Dust.NewDustPerfect(point.ToVector2(), DustID.Electric, Vector2.Zero);
                    dust.noGravity = true;
                    if (Projectile.Hitbox.Contains(point))
                    {
                        Projectile.Kill();
                    }
                }
                if (Projectile.ai[0] == 2f)
                {
                    Projectile.timeLeft = 30;
                    Projectile.ai[0] = 3f;
                    Projectile.netUpdate = true;
                }
                if (Projectile.timeLeft == 1 && Projectile.ai[0] == 3f)
                {
                    SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                    Projectile.timeLeft = 60;
                    Projectile.penetrate = 1;
                    Projectile.velocity *= -1;
                    Projectile.ai[0] = 4f;
                    Projectile.netUpdate = true;
                }
                if (Projectile.ai[0] == 4f)
                {
                    float maxDetectRadius = 400f;

                    NPC closestNPC = Projectile.FindClosestNPC(null, maxDetectRadius);
                    if (closestNPC == null)
                        return;

                    Projectile.Track(closestNPC, inertia: 8f);
                    Projectile.netUpdate = true;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.ai[0] == 1f)
            {
                Projectile.ai[0] = 5f;
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Player player = Main.player[Projectile.owner];
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                if (Projectile.ai[0] == 1f)
                {
                    int damage = Projectile.damage;
                    damage /= 3;
                    for (int i = 0; i < 6; i++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(60 * i)) * 16,
                            Type, damage, Projectile.knockBack, player.whoAmI, 2f);
                        Projectile.netUpdate = true;
                    }
                }
            }
        }
    }
}
