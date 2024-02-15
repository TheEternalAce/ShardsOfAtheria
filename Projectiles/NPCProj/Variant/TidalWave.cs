using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant
{
    public class TidalWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;

            DrawOffsetX = -5;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (SoA.Eternity())
            {
                if (++Projectile.ai[0] >= 10)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AcidWater>(), Projectile.damage, 0);
                    Projectile.ai[0] = 0;
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Bleeding, 60);
        }
    }
}