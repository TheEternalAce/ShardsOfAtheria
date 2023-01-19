using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using MMZeroElements;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class StormSword : ModProjectile
    {
        public static Asset<Texture2D> glowmask;
        private double rotation;
        private int attacks = 4;

        public override string Texture => "ShardsOfAtheria/Items/Weapons/Melee/ValkyrieBlade";

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
            ProjectileElements.Metal.Add(Type);
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.damage = 37;

            Projectile.timeLeft = 121;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!NPC.AnyNPCs(ModContent.NPCType<NovaStellar>()))
            {
                Projectile.Kill();
            }

            if (attacks < 1)
            {
                Projectile.Kill();
            }

            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -40;
                DrawOriginOffsetX = 16;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -16;
            }

            if (Projectile.ai[1] == 0)
            {
                rotation += .05;
                if (rotation > MathHelper.TwoPi)
                {
                    rotation -= MathHelper.TwoPi;
                }
                Projectile.Center = player.Center + Vector2.One.RotatedBy(Projectile.ai[0] / 7f * MathHelper.TwoPi + rotation) * 225;
                Projectile.rotation = Vector2.Normalize(Projectile.Center - player.Center).ToRotation() + MathHelper.ToRadians(225);
                if (Projectile.timeLeft <= 40)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                    dust.noGravity = true;
                }
                if (Projectile.timeLeft == 1)
                {
                    Projectile.timeLeft = 121;
                    Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 6f;
                    Projectile.ai[1] = 1;
                }
            }
            if (Projectile.ai[1] == 1)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

                if (Projectile.timeLeft == 1)
                {
                    Projectile.ai[1] = 0;
                    Projectile.timeLeft = 121;
                    rotation += MathHelper.ToRadians(180);
                    attacks -= 1;
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 300);
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