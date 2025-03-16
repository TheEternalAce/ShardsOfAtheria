using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Spectrum
{
    public class SpectrumTrain : ModProjectile
    {
        public int segments = 0;
        Vector2 spawnPosition = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(2, 3);
            Projectile.AddElement(0);
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(3);
        }

        public override void OnSpawn(IEntitySource source)
        {
            spawnPosition = Projectile.Center;
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.light = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.SetVisualOffsets(20);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            if (segments > 0 && Projectile.ai[1] == 1)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnPosition, Projectile.velocity, Type + 1, Projectile.damage, Projectile.knockBack);
                segments--;
            }
            Projectile.ai[1] = 1;
            if (Projectile.timeLeft == 30)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + Projectile.velocity * 30, Vector2.Zero, Type + 2, Projectile.damage, Projectile.knockBack);
                Projectile.tileCollide = false;
            }
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
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);

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
