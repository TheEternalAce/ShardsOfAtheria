using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Projectiles.Ammo;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Sentry
{
    public class AreusTurret : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionSacrificable[Type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 38; // The width of projectile hitbox
            Projectile.height = 14; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.DamageType = DamageClass.Summon; // Is the projectile shoot by a ranged weapon?
            Projectile.sentry = true;
            Projectile.minionSlots = 1;
            Projectile.timeLeft = Projectile.SentryLifeTime;

            DrawOffsetX = -14;
            DrawOriginOffsetY = -20;
        }

        int shootTimer = 0;
        int shootCooldown = 0;
        NPC Target;

        public override void AI()
        {
            if (++Projectile.velocity.Y > 16)
            {
                Projectile.velocity.Y = 16;
            }

            Target = Projectile.FindTargetWithinRange(1000);
            if (Target != null)
            {
                if (Target.CanBeChasedBy())
                {
                    if (shootCooldown > 0)
                    {
                        shootCooldown--;
                    }
                    else if (++shootTimer % 10 == 0)
                    {
                        var position = Projectile.Center + new Vector2(0, -20);
                        Vector2 vel = Vector2.Normalize(Target.Center - position);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, vel * 15,
                                ModContent.ProjectileType<AreusBulletProj>(), Projectile.damage, Projectile.knockBack,
                                Projectile.owner);
                        SoundEngine.PlaySound(SoundID.Item11);
                        if (shootTimer >= 40)
                        {
                            shootCooldown = 60;
                            shootTimer = 0;
                        }
                    }
                }
                else
                {
                    shootCooldown = 60;
                    shootTimer = 0;
                }
            }
            else
            {
                shootCooldown = 60;
                shootTimer = 0;
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }

        public override void PostDraw(Color lightColor)
        {
            var gun = ModContent.Request<Texture2D>(Texture + "_Gun").Value;
            Rectangle rect = new(0, 0, 90, 42);
            SpriteEffects spriteEffects = SpriteEffects.None;
            Vector2 offset = new(0, -20 - Projectile.height / 2);
            Vector2 origin = new(35, 24);
            float rotation = MathHelper.PiOver4;
            if (Target != null)
            {
                rotation = (Target.Center - (Projectile.Center + new Vector2(0, -20))).ToRotation();
                if (Target.Center.X < Projectile.Center.X)
                {
                    spriteEffects = SpriteEffects.FlipVertically;
                    offset.Y = -33;
                }
            }
            Vector2 drawPos = Projectile.Center + offset - Main.screenPosition;

            Main.spriteBatch.Draw(
                gun,
                drawPos,
                rect,
                lightColor,
                rotation,
                origin,
                Projectile.scale,
                spriteEffects,
                0f);

            base.PostDraw(lightColor);
        }
    }
}
