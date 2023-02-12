using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class LightningBoltFriendly : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.GolfBallDyedViolet}";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 140;
        }

        Vector2 initialVel = Vector2.Zero;
        int initialDmg = 0;
        int DustTimer = 0;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 600);
            if (Projectile.ai[1] == 1)
            {
                Projectile.CallStorm(3);
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

                if (crit)
                {
                    Projectile.CallStorm(1, 5);
                }
            }
            Projectile.damage = (int)(Projectile.damage * 0.9f);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (Projectile.ai[1] == 1)
            {
                Projectile.CallStorm(3);
            }
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 4f;
            if (initialVel == Vector2.Zero)
            {
                initialVel = Projectile.velocity;
                initialDmg = Projectile.damage;
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

        public override void Kill(int timeLeft)
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
