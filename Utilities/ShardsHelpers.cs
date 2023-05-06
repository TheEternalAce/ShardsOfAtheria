using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsHelpers // General class full of helper methods
    {
        public static int DefaultMaxStack = 9999;

        public struct UpgrageMaterial
        {
            public Item item;
            public int requiredStack;

            public UpgrageMaterial(Item item, int stack)
            {
                this.item = item;
                requiredStack = stack;
            }
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

        internal static void Log(this string str, bool broadcastToChat = false)
        {
            if (broadcastToChat)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(str), Color.White);
            }
            Console.WriteLine(str);
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
    }
}
