using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class GunswordGun : ModProjectile
    {
        int shotTimer = 0;
        const int timerMax = 20;

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 180;

            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            NPC target = Projectile.FindClosestNPC(null, 200);
            bool validTarget = target != null;
            if (validTarget)
            {
                Vector2 vector = Vector2.Normalize(target.Center - Projectile.Center);
                float toRot = vector.ToRotation();
                Projectile.rotation = MathHelper.Lerp(Projectile.rotation, toRot - (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi),
                    MathHelper.ToRadians(5));

                if (++shotTimer >= timerMax)
                {
                    Projectile bullet = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center,
                        vector * 5f, ProjectileID.Bullet, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    bullet.DamageType = DamageClass.Magic;
                    SoundEngine.PlaySound(SoundID.Item11, Projectile.Center);
                    shotTimer = 0;
                }
            }
            else
            {
                Projectile.rotation += MathHelper.ToRadians(4) * Projectile.direction;
                shotTimer = 0;
            }

            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -11;
                DrawOriginOffsetY = -2;
            }
            else
            {
                DrawOffsetX = -11;
                DrawOriginOffsetY = -2;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit42, Projectile.Center);
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Lead);
            }
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Gold);
            }
        }
    }
}
