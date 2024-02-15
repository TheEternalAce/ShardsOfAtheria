using BattleNetworkElements.Elements;
using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Other;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Utilities
{
    public partial class ShardsHelpers
    {
        public static void Explode(this Projectile projectile, Vector2 position, int damage, bool hostile = false, int explosionSize = 120, bool dustParticles = true)
        {
            Projectile explosion = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), position, Vector2.Zero,
                ModContent.ProjectileType<ElementExplosion>(), damage, projectile.knockBack, projectile.owner);
            explosion.DamageType = projectile.DamageType;
            explosion.Size = new Vector2(explosionSize);
            explosion.hostile = hostile;
            if (dustParticles)
            {
                explosion.ai[1] = 1;
            }
            ScreenShake.ShakeScreen(6, 60);
            if (SoA.ElementModEnabled)
            {
                SetExplosionElements(projectile, explosion);
            }
        }
        [JITWhenModsEnabled("BattleNetworkElements")]
        private static void SetExplosionElements(Projectile proj, Projectile explosion)
        {
            BNGlobalProjectile elementExplosion = explosion.GetGlobalProjectile<BNGlobalProjectile>();
            if (proj.IsFire())
            {
                elementExplosion.isFire = true;
            }
            if (proj.IsAqua())
            {
                elementExplosion.isAqua = true;
            }
            if (proj.IsElec())
            {
                elementExplosion.isElec = true;
            }
            if (proj.IsWood())
            {
                elementExplosion.isWood = true;
            }
        }

        /// <summary>
        /// Finding the closest NPC to attack within maxDetectDistance range.
        /// If not found then returns null.
        /// </summary>
        /// <param name="maxDetectDistance"></param>
        /// <returns></returns>
        public static NPC FindClosestNPC(this Projectile projectile, float maxDetectDistance, params int[] blaclkistedWhoAmI)
        {
            return FindClosestNPC(projectile.position, maxDetectDistance, blaclkistedWhoAmI);
        }

        public static Player FindClosestPlayer(this Projectile projectile, float maxDetectDistance, params int[] blaclkistedWhoAmI)
        {
            return FindClosestPlayer(projectile.Center, maxDetectDistance, blaclkistedWhoAmI);
        }

        public static Player GetPlayerOwner(this Projectile projectile)
        {
            return Main.player[projectile.owner];
        }

        public static NPC GetNPCOwner(this Projectile projectile)
        {
            return Main.npc[projectile.owner];
        }
        public static NPC GetNPCOwner(this Projectile projectile, int i)
        {
            if (i > 2 || i < 0)
            {
                return null;
            }
            return Main.npc[(int)projectile.ai[i]];
        }

        public static void Track(this Projectile projectile, NPC npc, float speed = 16f, float inertia = 16f)
        {
            if (npc == null) return;
            if (!npc.CanBeChasedBy()) return;
            projectile.Track(npc.Center, speed, inertia);
        }
        public static void Track(this Projectile projectile, Vector2 position, float speed = 16f, float inertia = 16f)
        {
            // The immediate range around the target (so it doesn't latch onto it when close)
            Vector2 direction = position - projectile.Center;
            direction.Normalize();
            direction *= speed;

            projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
        }

        public static void SetVisualOffsets(this Projectile projectile, int spriteSize, bool center = false)
        {
            projectile.SetVisualOffsets(new Vector2(spriteSize), center);
        }
        public static void SetVisualOffsets(this Projectile projectile, Vector2 spriteSize, bool center = false)
        {
            // 32 is the sprite size (here both width and height equal)
            int HalfSpriteWidth = (int)spriteSize.X / 2;
            int HalfSpriteHeight = (int)spriteSize.Y / 2;

            int HalfProjWidth = projectile.width / 2;
            int HalfProjHeight = projectile.height / 2;

            ModProjectile Projectile = projectile.ModProjectile;

            if (center)
            {
                // Vanilla configuration for "hitbox in middle of sprite"
                Projectile.DrawOriginOffsetX = 0;
                Projectile.DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
                Projectile.DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
            }
            // Vanilla configuration for "hitbox towards the end"
            else if (projectile.spriteDirection == 1)
            {
                Projectile.DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth);
                Projectile.DrawOffsetX = (int)-Projectile.DrawOriginOffsetX * 2;
                Projectile.DrawOriginOffsetY = 0;
            }
            else
            {
                Projectile.DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth);
                Projectile.DrawOffsetX = 0;
                Projectile.DrawOriginOffsetY = 0;
            }
        }

        public static void ApplyGravity(this Projectile projectile, ref int delay, float maxGravity = 16f)
        {
            if (--delay <= 0)
            {
                projectile.ApplyGravity(maxGravity);
            }
        }
        public static void ApplyGravity(this Projectile projectile, float maxGravity = 16f)
        {
            if (++projectile.velocity.Y > maxGravity)
            {
                projectile.velocity.Y = maxGravity;
            }
        }

        /// <summary>
        /// Draws a basic single-frame glowmask for an item dropped in the world. Use in <see cref="Terraria.ModLoader.ModProjectile.PostDraw"/>
        /// </summary>
        //public static void BasicInWorldGlowmask(this Projectile projectile, Texture2D glowTexture, Color color,
        //    float rotation, float scale)
        //{
        //    float offsetX = 0f;
        //    float originOffsetX = 0f;
        //    float originOffsetY = 0f;
        //    if (projectile.ModProjectile != null)
        //    {
        //        var modProjectile = projectile.ModProjectile;
        //        offsetX = modProjectile.DrawOffsetX;
        //        originOffsetX = modProjectile.DrawOriginOffsetX;
        //        originOffsetY = modProjectile.DrawOriginOffsetY;
        //    }
        //    Vector2 origin = glowTexture.Size() * 0.5f;
        //    origin.X += originOffsetX;
        //    origin.Y += originOffsetY;

        //    Main.EntitySpriteDraw(
        //        glowTexture,
        //        new Vector2(
        //            projectile.position.X - Main.screenPosition.X + projectile.width * 05f + offsetX,
        //            projectile.position.Y - Main.screenPosition.Y + projectile.height * 05f
        //        ),
        //        new Rectangle(0, 0, glowTexture.Width, glowTexture.Height),
        //        color,
        //        rotation,
        //        origin,
        //        scale,
        //        SpriteEffects.None,
        //        0);
        //}

        public const string Diamond = "DiamondBlur";
        public const string Orb = "OrbBlur";
        public const string Line = "LineTrail";
        public static void DrawBlurTrail(this Projectile projectile, Color color, string style, float angleAdd = 0f, float scale = 1f)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("ShardsOfAtheria/Assets/BlurTrails/" + style).Value;
            if (style == Diamond || style == Line)
            {
                angleAdd += MathHelper.PiOver2;
            }

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 offset = new(projectile.width / 2f, projectile.height / 2f);
                var frame = texture.Frame(1, 1, 0, 0);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + offset;
                float sizec = scale * (projectile.oldPos.Length - k) / (projectile.oldPos.Length * 0.8f);
                Color drawColor = color * (1f - projectile.alpha) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, projectile.oldRot[k] + angleAdd, frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
        }
        //Credits to Aslysmic/Tewst Mod (so cool)

        public static void DrawAfterImage(this Projectile projectile, Color color, Texture2D texture)
        {
            Rectangle frame = projectile.Frame();
            Vector2 offset = new(projectile.width / 2, projectile.height / 2);
            var effects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = frame.Size() / 2f;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                float progress = 1f / ProjectileID.Sets.TrailCacheLength[projectile.type] * i;
                Main.spriteBatch.Draw(texture, projectile.oldPos[i] + offset - Main.screenPosition, frame, color * (1f - progress), projectile.rotation, origin, Math.Max(projectile.scale * (1f - progress), 0.1f), effects, 0f);
            }

            Main.spriteBatch.Draw(texture, projectile.position + offset - Main.screenPosition, frame, color, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
        }
        public static void DrawPrimsAfterImage(this Projectile projectile, Color color)
        {
            var texture = TextureAssets.Projectile[projectile.type].Value;
            projectile.DrawAfterImage(color, texture);
        }
        //Credits to Aequus Mod (Omega Starite my beloved)

        public static SoAGlobalProjectile Atheria(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<SoAGlobalProjectile>();
        }

        public static void AddAreus(this Projectile projectile, bool dark = false, bool forceAddElements = false)
        {
            projectile.type.AddAreusProj(dark);
            if (!dark || forceAddElements)
            {
                projectile.AddElement(2);
                projectile.AddRedemptionElement(7);
            }
        }
        public static void AddAreusProj(this int projID, bool dark)
        {
            SoAGlobalProjectile.AreusProj.Add(projID, dark);
        }
        public static bool IsAreus(this Projectile projectile, bool includeDark)
        {
            if (!includeDark)
            {
                SoAGlobalProjectile.AreusProj.TryGetValue(projectile.type, out var dark);
                return SoAGlobalProjectile.AreusProj.ContainsKey(projectile.type) && !dark;
            }
            else return SoAGlobalProjectile.AreusProj.ContainsKey(projectile.type);
        }

        public static void SpearHoming(this Projectile projectile, float num1)
        {
            if (projectile.localAI[1] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity, num1);
                projectile.localAI[1] = 1f;
            }
            Vector2 vector = Vector2.Zero;
            float maxDistance = 200f;
            bool foundTarget = false;
            foreach (var npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    Vector2 vector2 = npc.Center - projectile.Center;
                    float distance = (float)Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y);
                    if (distance < maxDistance)
                    {
                        vector = vector2;
                        maxDistance = distance;
                        foundTarget = true;
                    }
                }
            }
            if (foundTarget)
            {
                AdjustMagnitude(ref vector, num1);
                projectile.velocity = (30f * projectile.velocity + vector) / 2f;
                AdjustMagnitude(ref projectile.velocity, num1);
            }
        }

        /// <summary>
        /// 0 (Fire) <br/>
        /// 1 (Aqua) <br/>
        /// 2 (Elec) <br/>
        /// 3 (Wood)
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="elementID"></param>
        public static void AddElement(this Projectile projectile, int elementID)
        {
            SoA.TryElementCall("assignElement", projectile, elementID);
        }

        /// <summary>
        /// 1	(Arcane) <br/>
        /// 2	(Fire) <br/>
        /// 3	(Water) <br/>
        /// 4	(Ice) <br/>
        /// 5	(Earth) <br/>
        /// 6	(Wind) <br/>
        /// 7	(Thunder) <br/>
        /// 8	(Holy) <br/>
        /// 9	(Shadow) <br/>
        /// 10	(Nature) <br/>
        /// 11	(Poison) <br/>
        /// 12	(Blood) <br/>
        /// 13	(Psychic) <br/>
        /// 14	(Celestial) <br/>
        /// 15	(Exposive)
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="elementID"></param>
        public static void AddRedemptionElement(this Projectile projectile, int elementID)
        {
            SoA.TryRedemptionCall("addElementProj", elementID, projectile.type);
        }
    }
}
