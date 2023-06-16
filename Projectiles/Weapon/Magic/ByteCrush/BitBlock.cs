using BattleNetworkElements.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic.ByteCrush
{
    public class BitBlock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElec();
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
        }
    }
}
