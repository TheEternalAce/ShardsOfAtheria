using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class AreusJavelinThrown : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 240;
            Projectile.penetrate = 2;
        }

        NPC targetNPC = null;
        int gravityTimer = 60;
        int delay;

        public override void AI()
        {
            float maxDetectRadius = -1f; // The maximum radius at which a projectile can detect a target
            float speed = 32f;

            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.SetVisualOffsets(78);
            Projectile.ApplyGravity(ref gravityTimer);
            if (Projectile.penetrate == 2)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

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
                    Projectile.Track(targetNPC, maxDetectRadius, speed);
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
                    SetSpin(false);
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
                    Projectile.Track(targetNPC, maxDetectRadius, speed);
                }
                else if (delay <= 50)
                {
                    Projectile.rotation += MathHelper.ToRadians(30);
                    if (Projectile.velocity.Length() != 0)
                    {
                        SetSpin(true);
                        Projectile.velocity.X = (float)Math.Floor(Projectile.velocity.X);
                        Projectile.velocity.Y = (float)Math.Floor(Projectile.velocity.Y);
                        int slowdown = 1;
                        if (Projectile.velocity.X != 0)
                        {
                            if (Projectile.velocity.X < 0)
                            {
                                Projectile.velocity.X += slowdown;
                            }
                            else
                            {
                                Projectile.velocity.X -= slowdown;
                            }
                        }
                        if (Projectile.velocity.Y != 0)
                        {
                            if (Projectile.velocity.Y < 0)
                            {
                                Projectile.velocity.Y += slowdown;
                            }
                            else
                            {
                                Projectile.velocity.Y -= slowdown;
                            }
                        }
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
                Projectile.friendly = false;
                delay = 60;
            }
        }

        void SetSpin(bool spin)
        {
            var oldCenter = Projectile.Center;
            if (spin)
            {
                Projectile.friendly = false;
                Projectile.Size = new(78);
            }
            else
            {
                Projectile.friendly = true;
                Projectile.Size = new(20);
            }
            Projectile.Center = oldCenter;
        }
    }
}