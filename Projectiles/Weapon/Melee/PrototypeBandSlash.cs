using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class PrototypeBandSlash : ModProjectile
    {
        public const int FadeInDuration = 7;
        public const int FadeOutDuration = 4;

        public const int TotalDuration = 16;

        // The "width" of the blade
        public float CollisionWidth => 10f * Projectile.scale;

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

            Projectile.AddAreus();
            Projectile.AddElementElec();
        }

        public override void SetDefaults()
        {
            Projectile.width = 92;
            Projectile.height = 92;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            Projectile.timeLeft = 12;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

            DrawOriginOffsetY = -39;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Timer += 1;
            if (Timer >= TotalDuration)
            {
                Projectile.Kill();
                return;
            }

            Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);

            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() +
                (Projectile.spriteDirection == 1 ? MathHelper.ToRadians(-45f) :
                MathHelper.ToRadians(225f));
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            }
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            var texture = TextureAssets.Projectile[Projectile.type].Value;
            int max = 10;
            int min = -10;
            Vector2 shake = new(Main.rand.Next(min, max), Main.rand.Next(min, max));
            return ModifiedDraw(texture, shake, lightColor);
        }

        private bool ModifiedDraw(Texture2D texture, Vector2 shake, Color color)
        {
            Rectangle frame = new(0, 0, texture.Width, texture.Height);
            Vector2 offset = new(Projectile.width / 2, Projectile.height / 2);
            var effects = Projectile.spriteDirection == 1 ?
                SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = frame.Size() / 2f;
            var drawPos = Projectile.position + offset - Main.screenPosition + shake;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
            {
                float progress = 1f / ProjectileID.Sets.TrailCacheLength[Projectile.type] * i;
                int max = 10;
                int min = -10;
                Vector2 shake2 = new(Main.rand.Next(min, max), Main.rand.Next(min, max));
                var drawTrailPos = Projectile.oldPos[i] + offset - Main.screenPosition + shake2;
                Main.spriteBatch.Draw(texture, drawTrailPos, frame, color * (1f - progress), Projectile.rotation, origin, Math.Max(Projectile.scale * (1f - progress), 0.1f), effects, 0f);
            }

            Main.spriteBatch.Draw(
                texture,
                drawPos,
                null,
                Color.White,
                Projectile.rotation,
                origin,
                Projectile.scale,
                effects,
                0f);
            return false;
        }
    }
}
