using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AreusJavelinThrown : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 240;
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 3;
        }

        NPC targetNPC = null;
        int gravityTimer = 60;
        int delay;
        bool Spinning
        {
            get => !Projectile.friendly;
            set => Projectile.friendly = !value;
        }

        public override void AI()
        {
            float maxDetectRadius = 1000f; // The maximum radius at which a projectile can detect a target
            float speed = 16f;

            Projectile.SetVisualOffsets(78, true);
            Projectile.ApplyGravity(ref gravityTimer);
            if (Projectile.penetrate == 2)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

                var player = Main.player[Projectile.owner];
                var shards = player.Shards();
                if (shards.Overdrive && gravityTimer <= 40)
                {
                    // Trying to find NPC closest to the projectile
                    if (targetNPC == null || !targetNPC.CanBeChasedBy())
                    {
                        targetNPC = Projectile.FindClosestNPC(maxDetectRadius);
                        return;
                    }
                    Projectile.Track(targetNPC, maxDetectRadius, speed, 8);
                }
            }
            else
            {
                if (delay > 0)
                {
                    delay--;
                }
                if (delay == 0)
                {
                    if (Spinning)
                    {
                        SetSpin(false);
                    }
                    Projectile.Track(targetNPC, maxDetectRadius, speed, 4);
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
                }
                else if (delay <= 50)
                {
                    Projectile.rotation += MathHelper.ToRadians(30);
                    if (Projectile.velocity.Length() != 0)
                    {
                        SetSpin(true);
                        Projectile.SlowDown();
                    }
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.CanBeChasedBy())
            {
                targetNPC = target;
                Spinning = true;
                delay = 60;
            }
        }

        void SetSpin(bool spin)
        {
            var oldCenter = Projectile.Center;
            Spinning = spin;
            Projectile.Center = oldCenter;
        }
    }
}