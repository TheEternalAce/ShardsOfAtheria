using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class GunCrimson : ModProjectile
    {
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
            Projectile.rotation += 0.4f * (float)Projectile.direction;
        }
    }
}
