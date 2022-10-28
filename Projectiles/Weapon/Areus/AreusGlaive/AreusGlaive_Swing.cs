using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok.IceStuff;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus.AreusGlaive
{
    public class AreusGlaive_Swing : ModProjectile
    {
        public float rotation = 45;

        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.AreusProjectile.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 75;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Melee;

            DrawOffsetX = -6;
            DrawOriginOffsetY = -16;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;

            // Centering
            Vector2 value21 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                value21.X = player.bodyFrame.Width - value21.X;
            }
            if (player.gravDir != 1f)
            {
                value21.Y = player.bodyFrame.Height - value21.Y;
            }
            value21 -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter - new Vector2(20f, 42f) / 2f + value21, reverseRotation: false, addGfxOffY: false) - Projectile.velocity;

            // Behavior
            if (player.whoAmI == Projectile.owner)
            {
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] > 70f)
                {
                    Projectile.Kill();
                }
                else player.itemAnimation = 10;

                int newDirection = Projectile.Center.X > player.Center.X ? -1 : 1;
                player.ChangeDir(newDirection);
                Projectile.direction = newDirection;

                if (Projectile.ai[0] == 35f)
                    SoundEngine.PlaySound(SoundID.Item1);
                if (Projectile.ai[0] > 35f)
                    rotation -= 6;
                else
                    rotation += 6;
                if (Projectile.direction == 1)
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(rotation + 135);
                else Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.ToRadians(rotation - 45);

            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float ro = Projectile.rotation + MathHelper.ToRadians(90);
            float collisionPoint4 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Hitbox.Center.ToVector2(),
                Projectile.Center + Projectile.velocity + ro.ToRotationVector2().SafeNormalize(Vector2.Zero) * 80f, 16f * Projectile.scale, ref collisionPoint4))
            {
                return true;
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;

            var drawPosition = Main.MouseWorld;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            if (Projectile.alpha == 0 && player.whoAmI == Projectile.owner)
            {
                int direction = -1;

                if (Projectile.Center.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            //TODO Generic glowmask draw, maybe generalize method
            Player player = Main.player[Projectile.owner];

            int offsetY = 0;
            int offsetX = 0;
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            float originX = (texture.Width - Projectile.width) * 0.5f + Projectile.width * 0.5f;
            ProjectileLoader.DrawOffset(Projectile, ref offsetX, ref offsetY, ref originX);

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.ai[0] > 35)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Vector2 drawPos = new Vector2(Projectile.position.X - Main.screenPosition.X + originX + offsetX, Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + Projectile.gfxOffY);
            Rectangle sourceRect = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Color glowColor = new Color(255, 255, 255, 255) * 0.7f * Projectile.Opacity;
            Vector2 drawOrigin = new Vector2(originX, Projectile.height / 2 + offsetY);
            Main.EntitySpriteDraw(texture, drawPos, sourceRect, glowColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
        }
    }
}