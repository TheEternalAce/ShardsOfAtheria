using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using MMZeroElements;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class StormLance : ModProjectile
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
            ProjectileElements.Metal.Add(Type);
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.damage = 37;

            Projectile.timeLeft = 90;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            NPC npc = Main.npc.FirstOrDefault(x => x.active && x.type == ModContent.NPCType<NovaStellar>());
            if (npc == null)
            {
                Projectile.Kill();
            }
            else
            {
                npc.spriteDirection = Projectile.Center.X > npc.Center.X ? 1 : -1;
                Projectile.Center = npc.Center + Projectile.velocity * 4;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
                if (Projectile.spriteDirection == 1)
                {
                    DrawOffsetX = -38;
                    DrawOriginOffsetX = 14;
                }
                else
                {
                    DrawOffsetX = 0;
                    DrawOriginOffsetX = -14;
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