using MMZeroElements;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class BlackHole : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;

            Projectile.timeLeft = 25;
        }
    }
}
