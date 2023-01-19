using MMZeroElements;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class GunCrimson : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.scale = .85f;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(10) && Projectile.ai[0] > 0)
            {
                Projectile bullet = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity.RotatedBy(Projectile.rotation) * 2, ProjectileID.Bullet, Projectile.damage, Projectile.knockBack, Projectile.owner);
                bullet.DamageType = DamageClass.Magic;
                SoundEngine.PlaySound(SoundID.Item11);
                Projectile.ai[1] = 0f;
                Projectile.ai[0]--;
            }
            Projectile.rotation += 0.4f * Projectile.direction;
        }
    }
}
