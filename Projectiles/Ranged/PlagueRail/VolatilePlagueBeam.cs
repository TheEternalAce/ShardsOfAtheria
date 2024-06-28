using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.PlagueRail
{
    public class VolatilePlagueBeam : PlagueBeam2
    {
        public override string Texture => ModContent.GetInstance<PlagueBeam2>().Texture;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            Projectile.ai[0] = 0;
        }
    }
}
