using ShardsOfAtheria.Globals;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class GunCorruption : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.MetalProj.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.4f * (float)Projectile.direction;
        }
    }
}
