using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant
{
    public class CactusNeedle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Projectile refProj = new Projectile();
            refProj.SetDefaults(ProjectileID.Bullet);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Bleeding, 60);
        }
    }
}