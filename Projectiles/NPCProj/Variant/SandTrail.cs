using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant
{
    public class SandTrail : ModProjectile
    {
        int gravityDelay = 16;

        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddRedemptionElement(5);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            Projectile.ApplyGravity(ref gravityDelay);

            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Sand);
            d.fadeIn = 1.3f;
            d.noGravity = true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Bleeding, 60);
        }
    }
}