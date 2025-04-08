using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Common.Projectiles
{
    public abstract class BasicBeam : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public int bounces = 0;
        public float dustFadeIn = 0f;
        public int dustDelay = 6;
        public int dustTimer = 0;
        public abstract int DustType { get; }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 99;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8;

            Vector2 spawnPos = Projectile.Center;
            if ((Math.Abs(spawnPos.X) > Main.screenWidth * 0.6f || Math.Abs(spawnPos.Y) > Main.screenHeight * 0.6f) && ++dustTimer >= dustDelay)
            {
                Dust d = Dust.NewDustPerfect(spawnPos, DustType, Vector2.Zero);
                d.velocity *= 0;
                d.fadeIn = dustFadeIn;
                d.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);

            if (bounces-- != 0)
            {
                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                    Projectile.velocity.X = -oldVelocity.X;

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                    Projectile.velocity.Y = -oldVelocity.Y;

                return false;
            }
            return true;
        }
    }
}
