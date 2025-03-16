using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class LightningBolt : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 200;
            Projectile.AddDamageType(5);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 6;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 22;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 140;
        }

        Vector2 initialVel = Vector2.Zero;

        public override void AI()
        {
            if (initialVel == Vector2.Zero)
                initialVel = Projectile.velocity;
            if (++Projectile.ai[0] > 4)
            {
                Projectile.velocity = initialVel.RotatedByRandom(MathHelper.ToRadians(35));
                Projectile.ai[0] = 0;
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<ElectricShock>(600);
            if (SoA.Eternity() && Main.rand.NextBool(5))
            {
                target.AddBuff(ModContent.Find<ModBuff>("FargowiltasSouls", "ClippedWingsBuff").Type, 600);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBloomTrail_NoDiminishingScale(SoA.ElectricColorA0, SoA.LineBloom, 0, 0.5f);
            return base.PreDraw(ref lightColor);
        }
    }
}
