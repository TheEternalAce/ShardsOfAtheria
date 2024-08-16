using Terraria;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AreusRailgunProj : ElectromagneticGun
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Ranged/AreusRailgun";
        public override string FlashPath => "ShardsOfAtheria/Projectiles/Ranged/AreusRailgunProj_Flash";

        public override void SetDefaults()
        {
            Projectile.width = 76;
            Projectile.height = 26;
            base.SetDefaults();
        }
    }
}
