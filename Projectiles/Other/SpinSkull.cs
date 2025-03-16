using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class SpinSkull : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(1);
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 102;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.rotation -= .4f;
            Projectile.Center = owner.Center;
        }
    }
}