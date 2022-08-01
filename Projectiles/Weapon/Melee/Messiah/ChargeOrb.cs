using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.Weapons.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.Messiah
{
    public class ChargeOrb : ModProjectile
    {
        public float rotation;
        public int charge;

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
            base.SetStaticDefaults();
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.ignoreWater = true;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CheckActive(player);
            rotation += .15f;
            Projectile.Center = player.Center + Vector2.One.RotatedBy(((Projectile.ai[0] / 3) * MathHelper.TwoPi) + rotation) * 15;

            if (++charge >= 120)
            {
                Projectile.frame = 1;
            }
        }

        private bool CheckActive(Player player)
        {
            if (player.dead || !player.active || !(player.HasItem(ModContent.ItemType<TheMessiah>()) || player.HeldItem.type == ModContent.ItemType<TheMessiah>()))
            {
                return false;
            }
            else Projectile.timeLeft = 2;
            return true;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Main.instance.DrawCacheProjsOverPlayers.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
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
