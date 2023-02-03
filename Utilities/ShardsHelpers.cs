using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;

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

        public static float Wave(float time, float minimum, float maximum)
        {
            return minimum + ((float)Math.Sin(time) + 1f) / 2f * (maximum - minimum);
        }

        internal static void Log(string str, bool broadcastToChat = false)
        {
            if (broadcastToChat)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(str), Color.White);
            }
            Console.WriteLine(str);
        }
    }

    //public static class Element
    //{
    //    public const int Null = -1;
    //    public const int Fire = 0;
    //    public const int Ice = 1;
    //    public const int Electric = 2;
    //    public const int Metal = 3;
    //    public const int Areus = 4;
    //}
}
