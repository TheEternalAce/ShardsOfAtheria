using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using MMZeroElements;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class LaserArrow : ModProjectile
    {
        public static Asset<Texture2D> glowmask;

        public override void Load()
        {
            glowmask = ModContent.Request<Texture2D>(Texture);
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
            ProjectileElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;

            AIType = ProjectileID.WoodenArrowFriendly; // Act exactly like default Arrow
        }

        public override void AI()
        {
            if (Projectile.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item12);
                Projectile.ai[1] = 1;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            return base.OnTileCollide(oldVelocity);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void PostDraw(Color lightColor)
        {
            //TODO Generic glowmask draw, maybe generalize method
            Player player = Main.player[Projectile.owner];

            int offsetY = 0;
            int offsetX = 0;
            Texture2D glowmaskTexture = glowmask.Value;
            float originX = (glowmaskTexture.Width - Projectile.width) * 0.5f + Projectile.width * 0.5f;
            ProjectileLoader.DrawOffset(Projectile, ref offsetX, ref offsetY, ref originX);

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (Projectile.ownerHitCheck && player.gravDir == -1f)
            {
                if (player.direction == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                else if (player.direction == -1)
                {
                    spriteEffects = SpriteEffects.None;
                }
            }

            Vector2 drawPos = new Vector2(Projectile.position.X - Main.screenPosition.X + originX + offsetX, Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + Projectile.gfxOffY);
            Rectangle sourceRect = glowmaskTexture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Color glowColor = new Color(255, 255, 255, 255) * 0.7f * Projectile.Opacity;
            Vector2 drawOrigin = new Vector2(originX, Projectile.height / 2 + offsetY);
            Main.EntitySpriteDraw(glowmaskTexture, drawPos, sourceRect, glowColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
        }
    }
}
