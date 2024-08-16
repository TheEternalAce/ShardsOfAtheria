using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AreusBounceShot : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
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
            Projectile.localNPCHitCooldown = 3;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
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

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8;
            if (++Projectile.ai[0] >= 6)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, Vector2.Zero);
                d.velocity *= 0;
                d.fadeIn = 1.3f;
                d.noGravity = true;

                //float b = (float)Math.Cos(Projectile.ai[1]);
                //var holy = ModContent.DustType<AreusDust>();
                //var dark = ModContent.DustType<AreusDust_Dark>();
                //float a = 6f;
                //var dust = Dust.NewDustPerfect(Projectile.Center + new Vector2(0f, b * a).RotatedBy(Projectile.velocity.ToRotation()), holy);
                //dust.noGravity = true;
                //dust.velocity *= 0f;
                //dust = Dust.NewDustPerfect(Projectile.Center + new Vector2(0f, -b * a).RotatedBy(Projectile.velocity.ToRotation()), dark);
                //dust.noGravity = true;
                //dust.velocity *= 0f;
                //Projectile.ai[1] += MathHelper.ToRadians(2.5f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            // If collide with tile, reduce the penetrate.
            // So the projectile can reflect at most 5 times
            if (++Projectile.ai[2] >= 3)
            {
                return true;
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
            }

            var npc = Projectile.FindClosestNPC(null, 100);
            if (npc != null)
            {
                var velocity = npc.Center - Projectile.Center;
                velocity.Normalize();
                Projectile.velocity = velocity *= 16f;
            }

            return false;
        }
    }
}
