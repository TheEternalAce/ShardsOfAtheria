using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic
{
    public class AreusBounceShot : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.GolfBallDyedViolet}";

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 3;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //int[] blacklist = new int[] { target.whoAmI, (int)Projectile.ai[0] };
            //var npc = Projectile.FindClosestNPC(500, blacklist);
            //if (npc != null)
            //{
            //    var velocity = npc.Center - Projectile.Center;
            //    velocity.Normalize();
            //    Projectile.velocity = velocity *= 16f;
            //    Projectile.ai[0] = target.whoAmI;
            //}
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void AI()
        {
            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // If collide with tile, reduce the penetrate.
            // So the projectile can reflect at most 5 times
            if (++Projectile.ai[0] >= 3)
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
            }

            return false;
        }
    }
}
