using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj
{
    public class AreusLeadShriek : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(12);
            Projectile.AddRedemptionElement(6);
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.scale = 0.1f;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 40;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.scale += 0.1f;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X = (int)(Projectile.Center.X - 50 * Projectile.scale);
            hitbox.Y = (int)(Projectile.Center.Y - 50 * Projectile.scale);
            hitbox.Width = (int)(100 * Projectile.scale);
            hitbox.Height = (int)(100 * Projectile.scale);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> circle = ModContent.Request<Texture2D>(SoA.Circle);
            Main.EntitySpriteDraw(circle.Value, Projectile.Center - Main.screenPosition, null, Color.White, 0, circle.Size() / 2, Projectile.scale * 0.25f, SpriteEffects.None);
            return base.PreDraw(ref lightColor);
        }
    }
}