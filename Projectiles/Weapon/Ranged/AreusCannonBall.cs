using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Projectiles.Bases;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
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
            if (player.itemAnimation > 1)
            {
                var item = player.HeldItem;
                Rectangle itemHitbox = new((int)player.itemLocation.X,
                    (int)player.itemLocation.Y,
                    player.itemWidth,
                    player.itemHeight);
                if (item.useStyle == ItemUseStyleID.Swing && !item.noMelee && !item.noUseGraphic)
                {
                    if (itemHitbox.Intersects(Projectile.Hitbox))
                    {
                        if (player.IsLocal() && Projectile.ai[0] == 0)
                        {
                            Projectile.velocity = Main.MouseWorld - Projectile.Center;
                            Projectile.velocity.Normalize();
                            Projectile.velocity *= 20;
                            SoundEngine.PlaySound(SoundID.NPCHit4);
                            Projectile.ai[0] = 1;
                            gravityTimer = 180;
                        }
                    }
                }
                else
                {
                    foreach (var proj in Main.projectile)
                    {
                        if (proj.ModProjectile is SwordProjectileBase &&
                            proj.owner == Projectile.owner &&
                            proj.active)
                        {
                            if (proj.ModProjectile.Colliding(proj.Hitbox, Projectile.Hitbox) == null)
                            {
                                if (player.IsLocal())
                                {
                                    Projectile.velocity = Main.MouseWorld - Projectile.Center;
                                    Projectile.velocity.Normalize();
                                    Projectile.velocity *= 20;
                                    gravityTimer = 16;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.velocity.Y = -12;
            Projectile.velocity.X = 5 * -Projectile.direction;
            gravityTimer = 16;
            Projectile.ai[0] = 0;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawPrimsAfterImage(lightColor);
            return true;
        }

        public override void Kill(int timeLeft)
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
