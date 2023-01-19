using Microsoft.Xna.Framework;
using MMZeroElements;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class PrometheusFire : ModProjectile
    {
        int gravityTimer = 0;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            ProjectileElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.arrow = false;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 120;
            Projectile.penetrate = 5;
        }

        public override void AI()
        {
            CorrectRotation();
            UpdateVisuals();
            if (Projectile.ai[0] == 1)
            {
                if (++gravityTimer >= 15) // Use a timer to wait 15 ticks before applying gravity.
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
                }
                if (Projectile.velocity.Y > 16f)
                {
                    Projectile.velocity.Y = 16f;
                }
            }
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch,
                    0, 0, 254, Scale: 0.3f);
                dust.velocity += Projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
        }

        public void CorrectRotation()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }

        public void UpdateVisuals()
        {
            // Loop through the 4 animation frames, spending 5 ticks on each.
            if (++Projectile.frameCounter >= 15)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire3, 10 * 60);
            if (Projectile.ai[0] == 0 && Projectile.penetrate == 1)
            {
                Projectile.ai[0] = 1;
                Projectile.timeLeft = 600;
                Projectile.penetrate = 1;
                Vector2 velocity = -Projectile.velocity;
                velocity.Normalize();
                Vector2 nextVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(45)) * (4f + Main.rand.Next(0, 4));
                Projectile.velocity = nextVelocity;

                for (int i = 0; i < 4; i++)
                {
                    nextVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(45)) * (4f + Main.rand.Next(0, 4));
                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, nextVelocity,
                        ModContent.ProjectileType<PrometheusFire>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
                    proj.timeLeft = 600;
                    proj.penetrate = 1;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = 1;
                Projectile.timeLeft = 600;
                Projectile.penetrate = 1;
                Vector2 velocity = Projectile.velocity;
                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    velocity.X = -oldVelocity.X * 1.05f;
                }
                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    velocity.Y = -oldVelocity.Y * 1.05f;
                }
                velocity.Normalize();
                Vector2 nextVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(45)) * (4f + Main.rand.Next(0, 4));
                Projectile.velocity = nextVelocity;

                for (int i = 0; i < 4; i++)
                {
                    nextVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(45)) * (4f + Main.rand.Next(0, 4));
                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, nextVelocity,
                        ModContent.ProjectileType<PrometheusFire>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
                    proj.timeLeft = 600;
                    proj.penetrate = 1;
                }
                return false;
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;

                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch,
                    0, 0, 254, Scale: 0.3f);
                dust2.velocity += Projectile.velocity * 0.5f;
                dust2.velocity *= 0.5f;
            }

            if (Projectile.ai[0] == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector2 velocity = Projectile.velocity;
                    velocity.Normalize();
                    Vector2 nextVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(45)) * (4f + Main.rand.Next(0, 4));
                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, nextVelocity,
                        ModContent.ProjectileType<PrometheusFire>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
                    proj.timeLeft = 600;
                    proj.penetrate = 1;
                }
            }
            base.Kill(timeLeft);
        }
    }
}
