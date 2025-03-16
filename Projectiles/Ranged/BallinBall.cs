using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class BallinBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20; // The length of old position to be recorded

            Projectile.AddDamageType(4);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged.TryThrowing();
            Projectile.tileCollide = true;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Projectile.GetPlayerOwner().Overdrive())
            {
                Projectile.penetrate = 1;
                Projectile.velocity *= 2;
                Projectile.ai[0] = 1;
            }
        }

        public override void AI()
        {
            if (Projectile.damage < 1)
            {
                Projectile.damage = 1;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.damage > 1)
            {
                Projectile.damage -= 10;
            }
            Projectile.scale += 0.1f;
            Projectile.alpha += 25;
            NPC closestNPC = Projectile.FindClosestNPC(null, 1000, target.whoAmI);
            if (closestNPC != null)
            {
                float oldLength = Projectile.velocity.Length();
                Projectile.velocity = Vector2.Normalize(closestNPC.Center - Projectile.Center);
                Projectile.velocity *= oldLength;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // If collide with tile, reduce the penetrate.
            // So the projectile can reflect at most 5 times
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

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

                if (Projectile.damage > 1)
                {
                    Projectile.damage -= 10;
                }
                Projectile.scale += 0.1f;
                Projectile.alpha += 25;
            }

            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawAfterImage(Color.White);
            if (Projectile.ai[0] == 1)
            {
                Projectile.DrawBloomTrail(Color.Yellow.UseA(50), SoA.OrbBloom);
            }
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            if (Projectile.ai[0] == 1)
            {
                if (!Projectile.GetPlayerOwner().IsLocal()) return;
                var explosion = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                    ModContent.ProjectileType<FieryExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                explosion.DamageType = Projectile.DamageType;
            }
        }
    }
}
