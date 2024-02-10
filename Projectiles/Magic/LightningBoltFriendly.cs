using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class LightningBoltFriendly : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 140;
        }

        internal Vector2 initialVel = Vector2.Zero;
        internal int DustTimer = 0;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
            if (Projectile.ai[1] == 1)
            {
                ShardsHelpers.CallStorm(Projectile.GetSource_FromThis(), Projectile.Center, 3,
                    (int)(Projectile.damage * 0.66f), Projectile.knockBack, Projectile.DamageType, Projectile.owner);
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

            DustTimer++;
            if (DustTimer > 17 || Projectile.ai[1] == 2)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric);
                d.velocity *= 0;
                d.fadeIn = 1.3f;
                d.noGravity = true;
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
    }
}
