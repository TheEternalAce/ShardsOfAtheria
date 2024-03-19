using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon
{
    public class AreusTagLaser : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 99;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8;
            if (++Projectile.ai[0] >= 5)
            {
                int type = DustID.Electric;
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, type);
                d.velocity *= 0;
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Main.player[Projectile.owner];
            player.MinionAttackTargetNPC = target.whoAmI;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBlurTrail(SoA.ElectricColor, SoA.LineBlur);
            return base.PreDraw(ref lightColor);
        }
    }
}
