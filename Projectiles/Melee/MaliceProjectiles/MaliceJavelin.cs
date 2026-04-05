using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.MaliceProjectiles
{
    public class MaliceJavelin : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(2, 5);
            Projectile.AddElement(1);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(4);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 4;
            Projectile.light = 1;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 4f;

            if (Projectile.ai[0] == 0) Projectile.ai[0] = MathHelper.PiOver2;
            Projectile.ai[0] += MathHelper.ToRadians(5f);
            Projectile.SetVisualOffsets(52);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

            Vector2 offset = Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.PiOver2 + Projectile.ai[0]) * 4f;
            Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, DustID.ShadowbeamStaff);
            dust.velocity *= 0f;
            dust.noGravity = true;

            dust = Dust.NewDustPerfect(Projectile.Center - offset, DustID.ShadowbeamStaff);
            dust.velocity *= 0f;
            dust.noGravity = true;

            NPC target = ShardsHelpers.FindClosestNPC(Projectile.Center, null, 200f);
            if (target != null) Projectile.Track(target, 4f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff<MaliceDebuff>(300);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.ShadowbeamStaff);
            }
        }
    }
}
