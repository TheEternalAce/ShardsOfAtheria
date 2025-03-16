using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar
{
    public class ChargeLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(5);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 8;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 10;
            Projectile.timeLeft = 600;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return lightColor.MaxRGBA(120);
        }

        public override void OnSpawn(IEntitySource source)
        {
            var sound = SoundID.Item122;
            sound.PitchVariance = 0.1f;
            SoundEngine.PlaySound(sound, Projectile.Center);
            if (Projectile.GetPlayerOwner().HasBuff<ChargingDrones>()) Projectile.GetPlayerOwner().ClearBuff<ChargingDrones>();
        }

        public override void AI()
        {
            float progress = 1f - Projectile.ai[1] / (Projectile.timeLeft * 2);

            float val = 1f / (Projectile.extraUpdates + 1);
            Projectile.ai[1] += val;
            float opacityByProgress = 0f;
            if (progress < 0.2f)
            {
                opacityByProgress = 1f - progress / 0.2f;
            }
            else if (progress > 0.8f)
            {
                opacityByProgress = (progress - 0.8f) / 0.2f;
            }
            Projectile.Opacity = 1f - opacityByProgress;
            var dir = Projectile.velocity;
            Projectile.localAI[0] = 1200;
            Projectile.localAI[1] = dir.ToRotation();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }

            float _ = 0f;
            Vector2 beamEndPos = Projectile.Center + Projectile.velocity * Projectile.localAI[0];
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, beamEndPos, 16f * Projectile.scale, ref _);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawLaser();
            return false;
        }

        public void DrawLaser()
        {
            float progress = Projectile.ai[1] / (Main.player[Projectile.owner].itemAnimationMax * 2f);
            var dir = Projectile.velocity;

            if (progress < 0.25f)
            {
                return;
            }
            progress -= 0.2f;
            progress /= 0.45f;
            progress = Math.Min(progress, 1f);
            var startPosition = Projectile.Center - Main.screenPosition + dir;
            var endPosition = Projectile.Center + dir * Projectile.localAI[0] - Main.screenPosition;
            float scale = Projectile.scale * progress;
            var color = Color.Yellow * progress * Projectile.Opacity;

            float rotation = dir.ToRotation() - MathHelper.PiOver2;
            var texture = TextureAssets.Projectile[Type].Value;
            var frame = texture.Frame(verticalFrames: 3, frameY: 0);
            var origin = new Vector2(frame.Width / 2f, 6f);
            Main.spriteBatch.Draw(texture, startPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, startPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0f);
            float segmentBit = (frame.Height / 2f + 2.9f) * scale;
            int segments = (int)((startPosition - endPosition).Length() / segmentBit);
            frame = frame.Frame(0, 1);
            ScreenCulling.SetPadding(100);
            origin.Y += 4.2f;
            for (int i = 1; i < segments; i++)
            {
                var drawCoords = startPosition + dir * segmentBit * i;
                if (!ScreenCulling.OnScreen(drawCoords))
                    return;
                Main.spriteBatch.Draw(texture, drawCoords, frame, color, rotation, origin, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(texture, drawCoords, frame, color, rotation, origin, scale, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, endPosition - dir * frame.Height / 2f * scale, frame.Frame(0, 1), color * 3f, rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
