using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Spectrum
{
    public class SpectrumTrainBody : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(0);
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(Type - 1);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            if (Projectile.timeLeft == 30) Projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Projectile.GetPlayerOwner();
            int manaGain = 1;
            player.statMana += manaGain;
            player.ManaEffect(manaGain);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // If collide with tile, reduce the penetrate.
            // So the projectile can reflect at most 5 times
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 10)
            {
                Projectile.Kill();
            }
            else
            {
                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    Projectile.velocity.X = -oldVelocity.X * 1.05f;
                }

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    Projectile.velocity.Y = -oldVelocity.Y * 1.05f;
                }

                Projectile.damage += 5;
            }

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            if (timeLeft == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch, -Projectile.velocity.X * 0.5f, -Projectile.velocity.Y * 0.5f);
                    dust.velocity *= 4f;
                }
            }
        }
    }
}
