using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class CataracniaEye : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.aiStyle = ProjAIStyleID.Explosive;

            AIType = ProjectileID.BouncyGrenade;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, TorchID.Yellow);
            ModContent.GetInstance<CameraFocus>().SetTarget("CataracniaEye", Projectile.Center, CameraPriority.Weak);
        }
    }
}
