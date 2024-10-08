using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public abstract class BladeAura : ModProjectile
    {
        public abstract Color AuraColor { get; }
        public abstract int OutterDust { get; }
        public abstract int InnerDust { get; }
        public virtual float ScaleMultiplier => 1f;
        public virtual float ScaleAdder => 1f;
        public virtual float HalfRotations => 1f;

        public override string Texture => "Terraria/Images/Projectile_972";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ownerHitCheck = true;
            Projectile.ownerHitCheckDistance = 300f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.usesOwnerMeleeHitCD = true;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Vanilla has several particles that can easily be used anywhere.
            // The particles from the Particle Orchestra are predefined by vanilla and most can not be customized that much.
            // Use auto complete to see the other ParticleOrchestraType types there are.
            // Here we are spawning the Excalibur particle randomly inside of the target's hitbox.
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);

            // You could also spawn dusts at the enemy position. Here is simple an example:
            // Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, ModContent.DustType<Content.Dusts.Sparkle>());

            // Set the target's hit direction to away from the player so the knockback is in the correct direction.
            hit.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : -1;
        }

        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            Player player = Main.player[Projectile.owner];
            float itemAnimationMax = Projectile.ai[1];
            float progress = Projectile.localAI[0] / itemAnimationMax * HalfRotations;
            float direction = Projectile.ai[0];
            float velocityRotation = Projectile.velocity.ToRotation();
            float adjustedRotation = MathHelper.Pi * direction * progress + velocityRotation + direction * MathHelper.Pi + player.fullRotation;
            Projectile.rotation = adjustedRotation;

            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter) - Projectile.velocity;
            Projectile.scale = ScaleAdder + progress * ScaleMultiplier;

            float num10 = Projectile.rotation + Main.rand.NextFloatDirection() * (MathHelper.Pi / 2f) * 0.7f;
            Vector2 vector2 = Projectile.Center + num10.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 vector3 = (num10 + direction * (MathHelper.Pi / 2f)).ToRotationVector2();
            if (InnerDust > -1 && Main.rand.NextFloat() * 2f < Projectile.Opacity)
            {
                Dust dust8 = Dust.NewDustPerfect(Projectile.Center + num10.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), InnerDust, vector3 * 1f);
                dust8.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
                dust8.noGravity = true;
                dust8.noLight = false;
            }
            if (OutterDust > -1 && Main.rand.NextFloat() * 1.5f < Projectile.Opacity)
            {
                var dust9 = Dust.NewDustPerfect(vector2, OutterDust, vector3 * 1f);
                dust9.noGravity = true;
                dust9.noLight = false;
            }
            var light = AuraColor.ToVector3() * 0.75f;
            Lighting.AddLight(Projectile.Center, light);

            Projectile.scale *= Projectile.ai[2];

            if (Projectile.localAI[0] >= itemAnimationMax)
            {
                Projectile.Kill();
            }

            for (float i = -MathHelper.PiOver4; i <= MathHelper.PiOver4; i += MathHelper.PiOver2)
            {
                Rectangle rectangle = Utils.CenteredRectangle(Projectile.Center + (Projectile.rotation + i).ToRotationVector2() * 70f * Projectile.scale, new Vector2(60f * Projectile.scale, 60f * Projectile.scale));
                Projectile.EmitEnchantmentVisualsAt(rectangle.TopLeft(), rectangle.Width, rectangle.Height);
            }
        }
        public override void CutTiles()
        {
            Vector2 vector2 = (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 60f * Projectile.scale;
            Vector2 vector3 = (Projectile.rotation + MathHelper.PiOver4).ToRotationVector2() * 60f * Projectile.scale;
            float num2 = 60f * Projectile.scale;
            Utils.PlotTileLine(Projectile.Center + vector2, Projectile.Center + vector3, num2, DelegateMethods.CutTiles);
        }

        public override bool? CanCutTiles() => true;

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
            Asset<Texture2D> asset = TextureAssets.Projectile[Projectile.type];
            Rectangle rectangle = asset.Frame(1, 4);
            Vector2 origin = rectangle.Size() / 2f;
            float num = Projectile.scale * 1.1f;
            SpriteEffects effects = (SpriteEffects)(!(Projectile.ai[0] >= 0f) ? 2 : 0);
            float num2 = Projectile.localAI[0] / Projectile.ai[1];
            float num3 = Utils.Remap(num2, 0f, 0.6f, 0f, 1f) * Utils.Remap(num2, 0.6f, 1f, 1f, 0f);
            float num4 = 0.975f;
            Color color6 = Lighting.GetColor(Projectile.Center.ToTileCoordinates());
            Vector3 val = color6.ToVector3();
            float fromValue = val.Length() / (float)Math.Sqrt(3.0);
            fromValue = Utils.Remap(fromValue, 0.2f, 1f, 0f, 1f);
            Color color = AuraColor;
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)rectangle, color * fromValue * num3, Projectile.rotation + Projectile.ai[0] * MathHelper.PiOver4 * -1f * (1f - num2), origin, num, effects, 0f);
            Color color2 = color.UseA(80);
            //Color color3 = new(227, 182, 245, 80);
            Color color4 = Color.White * num3 * 0.5f;
            color4.A = (byte)(float)(int)(color4.A * (1f - fromValue));
            Color color5 = color4 * fromValue * 0.5f;
            color5.G = (byte)(color5.G * fromValue);
            color5.B = (byte)(color5.R * (0.25f + fromValue * 0.75f));
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)rectangle, color5 * 0.15f, Projectile.rotation + Projectile.ai[0] * 0.01f, origin, num, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)rectangle, color2 * fromValue * num3 * 0.3f, Projectile.rotation, origin, num, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)rectangle, color2 * fromValue * num3 * 0.5f, Projectile.rotation, origin, num * num4, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)asset.Frame(1, 4, 0, 3), color2 * 0.6f * num3, Projectile.rotation + Projectile.ai[0] * 0.01f, origin, num, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)asset.Frame(1, 4, 0, 3), color2 * 0.5f * num3, Projectile.rotation + Projectile.ai[0] * -0.05f, origin, num * 0.8f, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, (Rectangle?)asset.Frame(1, 4, 0, 3), color2 * 0.4f * num3, Projectile.rotation + Projectile.ai[0] * -0.1f, origin, num * 0.6f, effects, 0f);
            for (float num5 = 0f; num5 < 8f; num5 += 1f)
            {
                float num6 = Projectile.rotation + Projectile.ai[0] * num5 * -MathHelper.TwoPi * 0.025f + Utils.Remap(num2, 0f, 1f, 0f, MathHelper.PiOver4) * Projectile.ai[0];
                Vector2 drawpos = vector + num6.ToRotationVector2() * (asset.Width() * 0.5f - 6f) * num;
                float num7 = num5 / 9f;
                DrawPrettyStarSparkle(Projectile.Opacity, 0, drawpos, new Color(255, 255, 255, 0) * num3 * num7, color2, num2, 0f, 0.5f, 0.5f, 1f, num6, new Vector2(0f, Utils.Remap(num2, 0f, 1f, 3f, 0f)) * num, Vector2.One * num);
            }
            Vector2 drawpos2 = vector + (Projectile.rotation + Utils.Remap(num2, 0f, 1f, 0f, MathHelper.PiOver4) * Projectile.ai[0]).ToRotationVector2() * (asset.Width() * 0.5f - 4f) * num;
            DrawPrettyStarSparkle(Projectile.Opacity, 0, drawpos2, new Color(255, 255, 255, 0) * num3 * 0.5f, color2, num2, 0f, 0.5f, 0.5f, 1f, 0f, new Vector2(2f, Utils.Remap(num2, 0f, 1f, 4f, 1f)) * num, Vector2.One * num);
            return false;
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