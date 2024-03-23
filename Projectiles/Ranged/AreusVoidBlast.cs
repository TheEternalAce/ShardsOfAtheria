using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AreusVoidBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(9);
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 24;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 3;
            Projectile.ignoreWater = true;
            Projectile.arrow = true;
            Projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Projectile.GetPlayerOwner();
            if (!player.HasBuff<ShadeState>())
            {
                var areus = player.Areus();
                areus.imperialVoid -= 3;
            }
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.SetVisualOffsets(new Vector2(44, 28));
            DrawOriginOffsetY = -2;

            float maxDetectRange = 400;
            int npcWhoAmI = Projectile.FindTargetWithLineOfSight(maxDetectRange);
            if (npcWhoAmI != -1)
            {
                Projectile.Track(Main.npc[npcWhoAmI]);
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Explode(Projectile.Center, 200, dustParticles: false);
            DustRing();
        }

        private void DustRing()
        {
            for (var i = 0; i < 30; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                int type = DustID.Electric;
                if (Main.rand.NextBool())
                {
                    type = DustID.ShadowbeamStaff;
                    speed *= 2.25f;
                }
                Dust d = Dust.NewDustPerfect(Projectile.Center, type, speed * 6f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }

        public override void PostDraw(Color lightColor)
        {
            Projectile.DrawBloomTrail(lightColor.UseA(50), SoA.OrbBloom);
            Projectile.DrawAfterImage(lightColor);
        }
    }
}
