using MMZeroElements;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class CrystalExplosion : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Blank";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                ScreenShake.ShakeScreen(6, 60);
                SoundEngine.PlaySound(SoundID.Item14);
                Projectile.ai[0] = 1;
            }
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.BlueCrystalShard,
                   Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            dust.velocity += Projectile.velocity * 0.3f;
            dust.velocity *= 0.2f;
            dust.noGravity = true;
            Dust dust1 = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.PinkCrystalShard,
                   Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            dust1.velocity += Projectile.velocity * 0.3f;
            dust1.velocity *= 0.2f;
            dust1.noGravity = true;
            Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.PurpleCrystalShard,
                   Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            dust2.velocity += Projectile.velocity * 0.3f;
            dust2.velocity *= 0.2f;
            dust2.noGravity = true;
        }
    }
}