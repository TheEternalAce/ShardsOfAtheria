using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class GunCorruption : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;

            DrawOffsetX = -2;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(10) && Projectile.ai[0] > 0)
            {
                Projectile bullet = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 2, ProjectileID.Bullet, Projectile.damage, Projectile.knockBack, Projectile.owner);
                bullet.DamageType = DamageClass.Magic;
                SoundEngine.PlaySound(SoundID.Item11);
                Projectile.ai[1] = 0f;
                Projectile.ai[0]--;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;
        }
    }
}
