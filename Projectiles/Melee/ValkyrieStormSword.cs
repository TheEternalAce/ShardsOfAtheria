﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class ValkyrieStormSword : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Melee/ValkyrieStorm";

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(5);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;

            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 25;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!CheckActive(player))
            {
                return;
            }
            Projectile.ai[2] = player.gravDir;
            Projectile.rotation += MathHelper.ToRadians(30);
            if (Projectile.rotation > MathHelper.TwoPi) Projectile.rotation -= MathHelper.TwoPi;
            if (++Projectile.ai[0] >= 60)
            {
                ShardsHelpers.ProjectileRing(Projectile.GetSource_FromThis(), Projectile.Center, 6, 1f, 14f, ModContent.ProjectileType<StormBlade>(),
                    Projectile.damage / 3, Projectile.knockBack, rotationAddition: MathHelper.ToRadians(15f));
                SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
                Projectile.ai[0] = 0;
            }
            Lighting.AddLight(Projectile.Center, SoA.ElectricColorV3 * 0.75f);
            if (player.ItemAnimationActive)
            {
                float speed = 25f;
                if (player.IsLocal()) Projectile.Track(Main.MouseWorld, speed, speed / 2);
            }
            else
            {
                Projectile.Track(player.Center);
                if (Projectile.Hitbox.Intersects(player.getRect()))
                {
                    Projectile.Kill();
                }
            }
        }

        bool CheckActive(Player player)
        {
            if (player == null)
            {
                return false;
            }
            if (player.dead || !player.active)
            {
                return false;
            }
            Projectile.timeLeft = 2;
            return true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float coneLength = 94f * Projectile.scale;
            float collisionRotation = MathHelper.TwoPi / 25f * Projectile.ai[0];
            float maximumAngle = MathHelper.PiOver4;
            float coneRotation = Projectile.rotation + collisionRotation;
            if (targetHitbox.IntersectsConeSlowMoreAccurate(Projectile.Center, coneLength, coneRotation, maximumAngle))
            {
                return true;
            }
            float backOfSwing = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] * 0.3f, Projectile.ai[1] * 0.5f, 1f, 0f);
            if (backOfSwing > 0f)
            {
                float coneRotation2 = coneRotation - MathHelper.PiOver4 * Projectile.ai[0] * backOfSwing;
                if (targetHitbox.IntersectsConeSlowMoreAccurate(Projectile.Center, coneLength, coneRotation2, maximumAngle))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 vector = Projectile.Center - Main.screenPosition;
            Asset<Texture2D> asset = TextureAssets.Projectile[972];
            Rectangle rectangle = asset.Frame(1, 4);
            Vector2 origin = rectangle.Size() / 2f;
            float num = Projectile.scale * 1.1f;
            float direction = Projectile.ai[2];
            SpriteEffects effects = (SpriteEffects)(!(direction >= 0f) ? 2 : 0);
            float num2 = 0.5f;
            //float num3 = 0.8f;
            float num3 = Utils.Remap(num2, 0f, 0.6f, 0f, 1f) * Utils.Remap(num2, 0.6f, 1f, 1f, 0f);
            float num4 = 0.975f;
            float rotation = Projectile.rotation - MathHelper.PiOver4;
            Color color6 = Lighting.GetColor(Projectile.Center.ToTileCoordinates());
            Vector3 val = color6.ToVector3();
            float fromValue = val.Length() / (float)Math.Sqrt(3.0);
            fromValue = Utils.Remap(fromValue, 0.2f, 1f, 0f, 1f);
            Color color = SoA.ElectricColor;
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)rectangle, color * fromValue * num3, rotation + direction * MathHelper.PiOver4 * -1f * (1f - num2), origin, num, effects, 0f);
            Color color2 = color.UseA(80);
            //Color color3 = new(227, 182, 245, 80);
            Color color4 = Color.White * num3 * 0.5f;
            color4.A = (byte)(float)(int)(color4.A * (1f - fromValue));
            Color color5 = color4 * fromValue * 0.5f;
            color5.G = (byte)(color5.G * fromValue);
            color5.B = (byte)(color5.R * (0.25f + fromValue * 0.75f));
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)rectangle, color5 * 0.15f, rotation + direction * 0.01f, origin, num, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)rectangle, color2 * fromValue * num3 * 0.3f, rotation, origin, num, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)rectangle, color2 * fromValue * num3 * 0.5f, rotation, origin, num * num4, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)asset.Frame(1, 4, 0, 3), color2 * 0.6f * num3, rotation + direction * 0.01f, origin, num, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)asset.Frame(1, 4, 0, 3), color2 * 0.5f * num3, rotation + direction * -0.05f, origin, num * 0.8f, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)asset.Frame(1, 4, 0, 3), color2 * 0.4f * num3, rotation + direction * -0.1f, origin, num * 0.6f, effects, 0f);
            for (float num5 = 0f; num5 < 8f; num5 += 1f)
            {
                float num6 = rotation + direction * num5 * -MathHelper.TwoPi * 0.025f + Utils.Remap(num2, 0f, 1f, 0f, MathHelper.PiOver4) * direction;
                Vector2 drawpos = vector + num6.ToRotationVector2() * (asset.Width() * 0.5f - 6f) * num;
                float num7 = num5 / 9f;
                DrawPrettyStarSparkle(Projectile.Opacity, 0, drawpos, new Color(255, 255, 255, 0) * num3 * num7, color2, num2, 0f, 0.5f, 0.5f, 1f, num6, new Vector2(0f, Utils.Remap(num2, 0f, 1f, 3f, 0f)) * num, Vector2.One * num);
            }
            Vector2 drawpos2 = vector + (rotation + Utils.Remap(num2, 0f, 1f, 0f, MathHelper.PiOver4) * direction).ToRotationVector2() * (asset.Width() * 0.5f - 4f) * num;
            DrawPrettyStarSparkle(Projectile.Opacity, 0, drawpos2, new Color(255, 255, 255, 0) * num3 * 0.5f, color2, num2, 0f, 0.5f, 0.5f, 1f, 0f, new Vector2(2f, Utils.Remap(num2, 0f, 1f, 4f, 1f)) * num, Vector2.One * num);
            return base.PreDraw(ref lightColor);
        }

        private static void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
        {
            Texture2D value = TextureAssets.Extra[98].Value;
            Color color = shineColor * opacity * 0.5f;
            color.A = 0;
            Vector2 origin = value.Size() / 2f;
            Color color2 = drawColor * 0.5f;
            float num = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
            Vector2 vector = new Vector2(fatness.X * 0.5f, scale.X) * num;
            Vector2 vector2 = new Vector2(fatness.Y * 0.5f, scale.Y) * num;
            color *= num;
            color2 *= num;
            Main.EntitySpriteDraw(value, drawpos, null, color, MathHelper.Pi / 2f + rotation, origin, vector, dir);
            Main.EntitySpriteDraw(value, drawpos, null, color, 0f + rotation, origin, vector2, dir);
            Main.EntitySpriteDraw(value, drawpos, null, color2, MathHelper.Pi / 2f + rotation, origin, vector * 0.6f, dir);
            Main.EntitySpriteDraw(value, drawpos, null, color2, 0f + rotation, origin, vector2 * 0.6f, dir);
        }
    }
}
