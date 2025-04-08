using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AreusBounceShot : BasicBeam
    {
        public override string Texture => SoA.BlankTexture;

        public override int DustType => DustID.Electric;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(5);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);

            SoAGlobalProjectile.Metalic.Add(Type, 1f);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.aiStyle = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            bounces = 3;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.GetPlayerOwner().Shards().Overdrive)
            {
                var copyHit = hit;
                copyHit.Damage /= 4;
                target.StrikeNPC(copyHit);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            // If collide with tile, reduce the penetrate.
            // So the projectile can reflect at most 5 times
            if (bounces-- != 0)
            {
                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                    Projectile.velocity.X = -oldVelocity.X * 1.05f;

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                    Projectile.velocity.Y = -oldVelocity.Y * 1.05f;
                var npc = Projectile.FindClosestNPC(null, 100);
                if (npc != null)
                {
                    var velocity = npc.Center - Projectile.Center;
                    velocity.Normalize();
                    Projectile.velocity = velocity *= 16f;
                }

                return false;
            }
            return true;
        }
    }
}
