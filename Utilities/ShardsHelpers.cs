using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace ShardsOfAtheria.Utilities
{
    public static partial class ShardsHelpers // General class full of helper methods
    {
        public static int DefaultMaxStack = 9999;

        public static string[] SetEmptyStringArray(int size)
        {
            string[] array = new string[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = "";
            }
            return array;
        }

        public static Projectile[] ProjectileRing(IEntitySource source, Vector2 center, int amount,
            float radius, float speed, int type, int damage, float knockback, int owner, float ai0 = 0f,
            float ai1 = 0f, float ai2 = 0f)
        {
            Projectile[] projectiles = new Projectile[amount];
            float rotation = MathHelper.ToRadians(360 / amount);
            for (int i = 0; i < amount; i++)
            {
                Vector2 position = center + Vector2.One.RotatedBy(rotation * i) * radius;
                Vector2 velocity = Vector2.Normalize(center - position) * speed;
                projectiles[i] = Projectile.NewProjectileDirect(source, position, velocity, type,
                    damage, knockback, owner, ai0, ai1, ai2);
            }
            return projectiles;
        }

        public static ref Vector2 SlowDown(this Projectile projectile, float slowdown = 1f)
        {
            return ref projectile.velocity.SlowDown(slowdown);
        }
        public static ref Vector2 SlowDown(this ref Vector2 vector, float slowdown = 1f)
        {
            float min = 0.01f;
            int xDir = vector.X >= 1 ? 1 : -1;
            int yDir = vector.Y >= 1 ? 1 : -1;
            if (Math.Abs(vector.X) != min)
            {
                vector.X -= slowdown * xDir;
                if (vector.X > min * xDir)
                {
                    vector.X = min * xDir;
                }
            }
            if (Math.Abs(vector.Y) != min)
            {
                vector.Y -= slowdown * yDir;
                if (vector.Y > min * yDir)
                {
                    vector.Y = min * yDir;
                }
            }
            return ref vector;
        }

        public static void DrawLine(this SpriteBatch sb, int thickness, Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan(edge.Y / edge.X);

            if (edge.X < 0)
            {
                angle = MathHelper.Pi + angle;
            }

            sb.Draw(TextureAssets.MagicPixel.Value,
                new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), thickness),
                null,
                color, angle, new Vector2(0, 500f),
                SpriteEffects.None, 0);
        }

        public static int ToHours(this int num)
        {
            return num * (int)Math.Pow(60, 3);
        }

        public static int ToMinutes(this int num)
        {
            return num * (int)Math.Pow(60, 2);
        }

        public static int ToSeconds(this int num)
        {
            return num * 60;
        }

        public static Color UseA(this Color color, int alpha) => new Color(color.R, color.G, color.B, alpha);

        public static Color UseA(this Color color, float alpha) => new Color(color.R, color.G, color.B, (int)(alpha * 255));

        public static void CappedMeleeScale(Projectile proj)
        {
            float scale = Main.player[proj.owner].CappedMeleeScale();
            if (scale != 1f)
            {
                proj.scale *= scale;
                proj.width = (int)(proj.width * proj.scale);
                proj.height = (int)(proj.height * proj.scale);
            }
        }

        public static int ScaleByProggression(int baseAmount = 1)
        {
            int multiplier = 1;
            if (NPC.downedMoonlord)
            {
                multiplier = 5;
            }
            else if (NPC.downedGolemBoss)
            {
                multiplier = 4;
            }
            else if (Main.hardMode)
            {
                multiplier = 3;
            }
            else if (NPC.downedBoss3)
            {
                multiplier = 2;
            }
            return baseAmount * multiplier;
        }
        public static StatModifier ScaleByProggression(StatModifier baseAmount)
        {
            int multiplier = 1;
            if (NPC.downedMoonlord)
            {
                multiplier = 5;
            }
            else if (NPC.downedGolemBoss)
            {
                multiplier = 4;
            }
            else if (Main.hardMode)
            {
                multiplier = 3;
            }
            else if (NPC.downedBoss3)
            {
                multiplier = 2;
            }
            return baseAmount * multiplier;
        }

        public static float Wave(float time, float minimum, float maximum)
        {
            return minimum + ((float)Math.Sin(time) + 1f) / 2f * (maximum - minimum);
        }

        public static bool DeathrayHitbox(Vector2 center, Rectangle targetHitbox, float rotation, float length, float size, float startLength = 0f)
        {
            return DeathrayHitbox(center, targetHitbox, rotation.ToRotationVector2(), length, size, startLength);
        }
        public static bool DeathrayHitbox(Vector2 center, Rectangle targetHitbox, Vector2 normal, float length, float size, float startLength = 0f)
        {
            return DeathrayHitbox(center + normal * startLength, center + normal * startLength + normal * length, targetHitbox, size);
        }
        public static bool DeathrayHitbox(Vector2 from, Vector2 to, Rectangle targetHitbox, float size)
        {
            float _ = float.NaN;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), from, to, size, ref _);
        }

        public static bool[] GetBoolArray(this TagCompound tagCompound, string key)
        {
            return tagCompound.Get<bool[]>(key);
        }

        public static Color MaxRGBA(this Color color, byte amt)
        {
            return color.MaxRGBA(amt, amt);
        }
        public static Color MaxRGBA(this Color color, byte amt, byte a)
        {
            return color.MaxRGBA(amt, amt, amt, a);
        }
        public static Color MaxRGBA(this Color color, byte r, byte g, byte b, byte a)
        {
            color.R = Math.Max(color.R, r);
            color.G = Math.Max(color.G, g);
            color.B = Math.Max(color.B, b);
            color.A = Math.Max(color.A, a);
            return color;
        }

        public static Vector2 RotateTowards(Vector2 currentPosition, Vector2 currentVelocity, Vector2 targetPosition, float maxChange)
        {
            float scaleFactor = currentVelocity.Length();
            float targetAngle = currentPosition.AngleTo(targetPosition);
            return currentVelocity.ToRotation().AngleTowards(targetAngle, maxChange).ToRotationVector2() * scaleFactor;
        }

        public static Rectangle Frame(this Rectangle rectangle, int frameX, int frameY, int sizeOffsetX = 0, int sizeOffsetY = 0)
        {
            return new Rectangle(rectangle.X + (rectangle.Width - sizeOffsetX) * frameX, rectangle.Y + (rectangle.Width - sizeOffsetY) * frameY, rectangle.Width, rectangle.Height);
        }
        public static Rectangle Frame(this Projectile projectile)
        {
            return TextureAssets.Projectile[projectile.type].Value.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
        }

        public static SpriteEffects GetSpriteEffect(this Projectile projectile)
        {
            return (-projectile.spriteDirection).ToSpriteEffect();
        }

        public static SpriteEffects ToSpriteEffect(this int value)
        {
            return value == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public static Vector2 GetPointInRegion(Rectangle region)
        {
            Vector2 result = new()
            {
                X = region.X + Main.rand.Next(region.Width + 1),
                Y = region.Y + Main.rand.Next(region.Height + 1)
            };
            return result;
        }

        public static void AddKey(this WeightedRandom<string> random, string key, params object[] args)
        {
            random.Add(Language.GetTextValue(key, args));
        }

        public static bool ContainsAny<T>(this IEnumerable<T> en, Predicate<T> predicate)
        {
            foreach (var t in en)
            {
                if (predicate(t))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ContainsAny<T>(this IEnumerable<T> en, T en2)
        {
            return ContainsAny(en, (t) => t.Equals(en2));
        }

        public static bool CheckTileCollision(Vector2 pos, Rectangle hitbox)
        {
            bool valid = false;
            if (pos.X > 50f && pos.X < Main.maxTilesX * 16 - 50 &&
                pos.Y > 50f && pos.Y < Main.maxTilesY * 16 - 50)
            {
                if (!Collision.SolidCollision(pos, hitbox.Width, hitbox.Height))
                {
                    valid = true;
                }
            }
            return valid;
        }

        public static NPC FindClosestNPC(Vector2 pos, float maxDist, params int[] blacklistedWhoAmI)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDist * maxDist;
            if (maxDist < 0)
            {
                sqrMaxDetectDistance = float.PositiveInfinity;
            }

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, pos);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        // Check if NPC.whoAmI is not blacklisted
                        if (!blacklistedWhoAmI.Contains(target.whoAmI))
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            closestNPC = target;
                        }
                    }
                }
            }

            return closestNPC;
        }
        public static Player FindClosestPlayer(this Vector2 position, float maxDetectDistance, params int[] blaclkistedWhoAmI)
        {
            Player closestPlayer = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
            if (maxDetectDistance < 0)
            {
                sqrMaxDetectDistance = float.PositiveInfinity;
            }

            // Loop through all Players(max always 255)
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player target = Main.player[k];
                // Check if Player able to be targeted. It means that Player is
                // 1. active and alive
                if (target.active && !target.dead)
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, position);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        // Check if NPC.whoAmI is not blacklisted
                        if (!blaclkistedWhoAmI.Contains(target.whoAmI))
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            closestPlayer = target;
                        }
                    }
                }
            }

            return closestPlayer;
        }
    }
}
