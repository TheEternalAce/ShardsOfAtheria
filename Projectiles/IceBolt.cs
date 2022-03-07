using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace ShardsOfAtheria.Projectiles
{
    public class IceBolt : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.arrow = true;
            Projectile.light = 1;
            Projectile.DamageType = DamageClass.Magic;
            AIType = ProjectileID.Bullet;
            DrawOffsetX = 10;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Ice,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 10 * 60);
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
    }
}
