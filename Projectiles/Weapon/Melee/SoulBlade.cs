using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class SoulBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.scale = 1.5f;

            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.arrow = false;
            Projectile.light = 1;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkFairy);
                }
                Projectile.ai[0] = 1;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkFairy);
            }
        }
    }
}
