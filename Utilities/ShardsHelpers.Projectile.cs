using BattleNetworkElements.Elements;
using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Common.Projectiles;
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
            if (dustParticles) explosion.ai[1] = 1;
            ScreenShake.ShakeScreen(6, 60);
            if (SoA.BNEEnabled) SetExplosionElements(projectile, explosion);
        }

        public static bool IsTrueMelee(this Projectile projectile)
        {
            return SoAGlobalProjectile.TrueMelee.Contains(projectile.type) || (projectile.DamageType.CountsAsClass(DamageClass.Melee) &&
              (projectile.aiStyle == 19 || projectile.aiStyle == 75 || projectile.aiStyle == 161 || projectile.GetPlayerOwner().heldProj == projectile.whoAmI ||
              projectile.ModProjectile is CoolSword || projectile.ModProjectile is BladeAura));
        }

        public static bool NonWhipSummon(this Projectile projectile)
        {
            return projectile.minion || projectile.sentry || ProjectileID.Sets.SentryShot[projectile.type] || ProjectileID.Sets.MinionShot[projectile.type];
        }

        public static void ScaleUp(Projectile proj)
        {
            float scale = Main.player[proj.owner].CappedMeleeScale();
            if (scale != 1f)
            {
                proj.scale *= scale;
                proj.width = (int)(proj.width * proj.scale);
                proj.height = (int)(proj.height * proj.scale);
            }
        }

        /// <summary>
        /// Finding the closest NPC to attack within maxDetectDistance range.
        /// If not found then returns null.
        /// </summary>
        /// <param name="maxDetectDistance"></param>
        /// <returns></returns>
        public static NPC FindClosestNPC(this Projectile projectile, Func<NPC, bool> additionalChecks, float maxDetectDistance, params int[] blaclkistedWhoAmI)
        {
            return FindClosestNPC(projectile.position, additionalChecks, maxDetectDistance, blaclkistedWhoAmI);
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

        public static void AdjustMagnitude(ref Vector2 vector, float num1)
        {
            float num = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
            if (num > 1.5f)
            {
                vector *= num1 / num;
            }
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

        public static void GetDrawInfo(this Projectile projectile, out Texture2D texture, out Vector2 offset, out Rectangle frame, out Vector2 origin, out int trailLength)
        {
            texture = TextureAssets.Projectile[projectile.type].Value;
            offset = projectile.Size / 2f;
            frame = projectile.Frame();
            origin = frame.Size() / 2f;
            trailLength = ProjectileID.Sets.TrailCacheLength[projectile.type];
        }
        public static void ApplyGravity(this Projectile projectile, ref int delay, float gravityStrength = 0.1f, float maxYVelocity = 16f)
        {
            if (--delay <= 0)
            {
                projectile.ApplyGravity(gravityStrength, maxYVelocity);
            }
        }
        public static void ApplyGravity(this Projectile projectile, float gravityStrength = 0.1f, float maxYVelocity = 16f)
        {
            projectile.velocity.Y += gravityStrength;
            maxYVelocity += maxYVelocity * projectile.GetPlayerOwner().GetTotalAttackSpeed(projectile.DamageType);
            if (projectile.velocity.Y > maxYVelocity)
            {
                projectile.velocity.Y = maxYVelocity;
            }
        }

        public static void CloneDefaults<T>(this Projectile projectile) where T : ModProjectile
        {
            projectile.CloneDefaults(ModContent.ProjectileType<T>());
        }

        public static void DrawBloomTrail(this Projectile projectile, Color color, Texture2D texture, float rotationToAdd = 0f, float scale = 1f)
        {
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 offset = new(projectile.width / 2f, projectile.height / 2f);
                var frame = texture.Frame(1, 1, 0, 0);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + offset;
                float sizec = scale * (projectile.oldPos.Length - k) / (projectile.oldPos.Length * 0.8f);
                Color drawColor = color * (1f - projectile.alpha) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, projectile.oldRot[k] + rotationToAdd, frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
        }

        public static void DrawBloomTrail_NoDiminishingScale(this Projectile projectile, Color color, Texture2D texture, float rotationToAdd = 0f, float scale = 1f)
        {
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 offset = new(projectile.width / 2f, projectile.height / 2f);
                var frame = texture.Frame(1, 1, 0, 0);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + offset;
                Color drawColor = color * (1f - projectile.alpha) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, projectile.oldRot[k] + rotationToAdd, frame.Size() / 2, scale, SpriteEffects.None, 0);
            }
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

            Main.spriteBatch.Draw(texture, projectile.position + offset - Main.screenPosition, frame, color, projectile.rotation, origin, projectile.scale, effects, 0f);
        }
        public static void DrawAfterImage(this Projectile projectile, Color color)
        {
            var texture = TextureAssets.Projectile[projectile.type].Value;
            projectile.DrawAfterImage(color, texture);
        }
        //Credits to Aequus Mod (Omega Starite my beloved)

        public static SoAGlobalProjectile Atheria(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<SoAGlobalProjectile>();
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

        public static void MakeTrueMelee(this Projectile projectile)
        {
            SoAGlobalProjectile.TrueMelee.Add(projectile.type);
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
        /// 0 (Acid) <br/>
        /// 1 (Bludgeoning) <br/>
        /// 2 (Cold) <br/>
        /// 3 (Fire) <br/>
        /// 4 (Force) <br/>
        /// 5 (Lightning) <br/>
        /// 6 (Necrotic) <br/>
        /// 7 (Piercing) <br/>
        /// 8 (Poison) <br/>
        /// 9 (Psychic) <br/>
        /// 10 (Radiant) <br/>
        /// 11 (Slashing) <br/>
        /// 12 (Thunder) <br/>
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="elementIDs"></param>
        public static void AddDamageType(this Projectile projectile, params int[] elementIDs)
        {
            SoA.TryDungeonCall("addDamageElement", "projectile", projectile.type, elementIDs);
        }

        /// <summary>
        /// 0 (Fire) <br/>
        /// 1 (Aqua) <br/>
        /// 2 (Elec) <br/>
        /// 3 (Wood)
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="elementIDs"></param>
        public static void AddElement(this Projectile projectile, params int[] elementIDs)
        {
            foreach (int elementID in elementIDs)
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
        public static void AddRedemptionElement(this Projectile projectile, params int[] elementIDs)
        {
            foreach (int elementID in elementIDs)
                SoA.TryRedemptionCall("addElementProj", elementID, projectile.type);
        }
    }
}
