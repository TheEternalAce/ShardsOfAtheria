using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Summon
{
    public class SoulDagger : ModProjectile
    {
        public static Asset<Texture2D> glowmask;
        private double rotation;

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
            ProjectileElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;

            Projectile.aiStyle = 0;
            Projectile.tileCollide = false;
            Projectile.light = 1;
            Projectile.penetrate = -1;

            DrawOffsetX = -5;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead)
            {
                Projectile.Kill();
            }

            if (Projectile.ai[1] == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkFairy);
                }
                Projectile.ai[1] = 1;
            }
            if (player == Main.player[Projectile.owner] && ShardsOfAtheria.ArmorSetBonusActive.JustPressed && Projectile.ai[1] == 1)
            {
                if (!Main.LocalPlayer.mouseInterface)
                {
                    Projectile.ai[1] = 2;
                    Projectile.timeLeft = 60;
                    Projectile.usesIDStaticNPCImmunity = true;
                    Projectile.ownerHitCheck = false;
                }
            }
            //Idle
            if (Projectile.ai[1] == 1)
            {
                rotation += .05;
                Projectile.Center = player.Center + Vector2.One.RotatedBy(((Projectile.ai[0] / 10f) * MathHelper.TwoPi) + rotation) * 45;
                Projectile.timeLeft = 60;
                Projectile.rotation = Vector2.Normalize(Projectile.Center - player.Center).ToRotation() + MathHelper.ToRadians(90f);
            }
            //Flying
            if (Projectile.ai[1] == 2 && player == Main.player[Projectile.owner])
            {
                Projectile.velocity = Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 20;
                Projectile.ai[1] = 3;
            }
            if (Projectile.ai[1] == 3)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkFairy);
            }
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
