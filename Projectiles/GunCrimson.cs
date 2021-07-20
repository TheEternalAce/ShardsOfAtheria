using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SagesMania.Projectiles
{
    public class GunCrimson : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 46;
            projectile.height = 46;
            projectile.scale = .85f;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            projectile.rotation += 0.4f * (float)projectile.direction;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 origin = sourceRectangle.Size() / 2f;

            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), sourceRectangle, drawColor, projectile.rotation, origin, 1, spriteEffects, 0f);

            return false;
        }
    }
}
