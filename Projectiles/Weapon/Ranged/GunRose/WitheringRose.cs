using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.GunRose
{
    public class WitheringRose : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAqua();
        }

        public override void SetDefaults()
        {
            Projectile refProj = new();
            refProj.SetDefaults(ProjectileID.FlowerPow);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 60)
            {
                ShardsHelpers.ProjectileRing(Projectile.GetSource_FromThis(), Projectile.Center,
                    5, 1, 16f, ModContent.ProjectileType<WitheringPetal>(), Projectile.damage,
                    Projectile.knockBack, Projectile.owner);
                Projectile.ai[0] = 0;
            }
        }
    }
}
