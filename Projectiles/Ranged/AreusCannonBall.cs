using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AreusCannonBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 1200;
        }

        int gravityTimer = 180;
        public override void AI()
        {
            Projectile.ApplyGravity(ref gravityTimer);

            var player = Projectile.GetPlayerOwner();
            if (player.Shards().Overdrive)
            {
                if (Projectile.timeLeft <= 1190 &&
                    Projectile.ai[0] == 0 &&
                    gravityTimer <= 0)
                {
                    float maxDetectDistance = 100f;
                    var offset = Main.MouseWorld - player.Center;
                    offset.Normalize();
                    offset *= 50;
                    float magnetizeSpeed = 8f;
                    float magnetizeInertia = 1f;
                    if (Projectile.Distance(player.Center) <= 80)
                    {
                        magnetizeSpeed *= 2f;
                        magnetizeInertia = 8f;
                    }
                    Projectile.Track(player.Center + offset, maxDetectDistance,
                        magnetizeSpeed, magnetizeInertia);
                }
            }
            if (player.itemAnimation > 1)
            {
                var item = player.HeldItem;
                if (item.useStyle == ItemUseStyleID.Swing && !item.noMelee && !item.noUseGraphic)
                {
                    if (Projectile.Distance(player.Center) <= 60 * item.scale)
                    {
                        if (player.IsLocal() && Projectile.ai[0] == 0)
                        {
                            HitCannonBall();
                        }
                    }
                }
                else
                {
                    foreach (var proj in Main.projectile)
                    {
                        //if (proj.aiStyle == ProjAIStyleID.Hook &&
                        //    proj.owner == Projectile.owner &&
                        //    proj.active)
                        //{
                        //    if (proj.Distance(player.Center) > 50)
                        //    {
                        //        if (proj.Colliding(proj.Hitbox, Projectile.Hitbox))
                        //        {
                        //            if (proj.ai[0] == 1)
                        //            {
                        //                Projectile.velocity = proj.velocity;
                        //            }
                        //            proj.ai[0] = 1f;
                        //            Projectile.Center = proj.Center;
                        //            gravityTimer = 2;
                        //        }
                        //    }
                        //}
                        if (proj.ModProjectile is CoolSword sword &&
                        proj.owner == Projectile.owner &&
                        proj.active)
                        {
                            if (sword.Colliding(proj.Hitbox, Projectile.Hitbox) == null)
                            {
                                if (player.IsLocal())
                                {
                                    HitCannonBall();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HitCannonBall()
        {
            Projectile.velocity = Main.MouseWorld - Projectile.Center;
            Projectile.velocity.Normalize();
            Projectile.velocity *= 20;
            SoundEngine.PlaySound(SoundID.NPCHit4);
            Projectile.ai[0] = 1;
            Projectile.ai[1]++;
            gravityTimer = 180;
            if (Projectile.ai[1] >= 3)
            {
                Projectile.damage += 50;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] >= 3)
            {
                Projectile.penetrate = -1;
            }
            else
            {
                Projectile.velocity.Y = -12;
                Projectile.velocity.X = 5 * -Projectile.direction;
                gravityTimer = 16;
                Projectile.ai[0] = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawPrimsAfterImage(lightColor);
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<AreusDust>());
            }
        }
    }
}
