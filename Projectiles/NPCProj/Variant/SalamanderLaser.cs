using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant
{
    public class SalamanderLaser : ModProjectile
    {
        public override string Texture => SoA.LineBlur;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.extraUpdates = 5;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.penetrate--;
        }

        public override void OnKill(int timeLeft)
        {
            if (SoA.Eternity())
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ElectricExplosion>(),
                    Projectile.damage, 0);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBlurTrail(Color.White, SoA.LineBlur);
            return base.PreDraw(ref lightColor);
        }
    }
}