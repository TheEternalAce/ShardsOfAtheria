using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SagesMania.Projectiles
{
    public class ElectricBlade : ModProjectile {
        public override void SetDefaults() {
            projectile.width = 32;
            projectile.height = 32;
            projectile.scale = 1.5f;

            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = false;
            projectile.light = 1;
            aiType = ProjectileID.NightBeam;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(BuffID.Electrified, 10*60);
        }
        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, DustID.Electric,
                    projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, DustID.Electric,
                    0, 0, 254, Scale: 0.3f);
                dust.velocity += projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
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
