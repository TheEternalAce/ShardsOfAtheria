using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class LightningBoltFriendly : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 200;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 20;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 140;
        }

        internal Vector2 initialVel = Vector2.Zero;

        public override void OnSpawn(IEntitySource source)
        {
            if (Projectile.ai[1] == 3f) Projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
            if (Projectile.ai[1] == 1)
            {
                ShardsHelpers.CallStorm(Projectile.GetSource_FromThis(), Projectile.Center, 3,
                    (int)(Projectile.damage * 0.66f), Projectile.knockBack, Projectile.DamageType);
            }
            else if (Projectile.ai[1] == 2 && target.CanBeChasedBy())
            {
                bool newTarget = false;
                Vector2 targetPos = Vector2.Zero;
                float distance = 2000;
                for (var i = 0; i < Main.maxNPCs; i++)
                {
                    NPC n = Main.npc[i];
                    if (n.active)
                    {
                        float dist = Vector2.Distance(Projectile.Center, n.Center);
                        if (dist > 80 && dist < distance && n.CanBeChasedBy())
                        {
                            newTarget = true;
                            targetPos = n.Center;
                            distance = dist;
                        }
                    }
                }
                if (newTarget)
                {
                    Projectile.timeLeft += 200;
                    Projectile.velocity = (targetPos - Projectile.Center).SafeNormalize(Vector2.Zero) * Projectile.velocity.Length();
                    initialVel = Projectile.velocity;
                }

                if (hit.Crit)
                {
                    ShardsHelpers.CallStorm(Projectile.GetSource_FromThis(), Projectile.Center, 1,
                        (int)(Projectile.damage * 0.66f), Projectile.knockBack, Projectile.DamageType, Projectile.owner, 5);
                }
            }
            Projectile.damage = (int)(Projectile.damage * 0.9f);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Projectile.ai[1] == 1)
            {
                ShardsHelpers.CallStorm(Projectile.GetSource_FromThis(), Projectile.Center, 3,
                    (int)(Projectile.damage * 0.66f), Projectile.knockBack, Projectile.DamageType, Projectile.owner);
            }
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 4f;
            if (initialVel == Vector2.Zero)
            {
                initialVel = Projectile.velocity;
            }
            if (++Projectile.ai[0] > 4)
            {
                Projectile.velocity = initialVel.RotatedByRandom(MathHelper.ToRadians(35));
                Projectile.ai[0] = 0;
            }
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.ai[1] == 3)
            {
                if (Projectile.GetPlayerOwner().Center.Y - 100 < Projectile.Center.Y)
                {
                    Projectile.tileCollide = true;
                    Projectile.ai[1] = 0;
                }
            }
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

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBloomTrail_NoDiminishingScale(SoA.ElectricColorA, SoA.LineBloom, 0, 0.5f);
            return base.PreDraw(ref lightColor);
        }
    }
}
