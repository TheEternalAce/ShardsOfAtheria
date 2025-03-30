using ShardsOfAtheria.Common.Projectiles;
using Terraria;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class CoilgunProj : ChargeGun
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Ranged/Coilgun";

        public override string FlashPath => "ShardsOfAtheria/Projectiles/Ranged/CoilgunProj_Flash";

        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 20;
            base.SetDefaults();
        }
    }
}
