using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class AreusEnergyBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 24;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 3;
            Projectile.ignoreWater = true;

            DrawOffsetX = -6;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Explode(Projectile.Center, Projectile.damage, dustParticles: false);
            ShardsHelpers.DustRing(Projectile.Center, 6, DustID.Electric);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBlurTrail(SoA.ElectricColor, SoA.DiamondBlur);
            return false;
        }
    }
}
