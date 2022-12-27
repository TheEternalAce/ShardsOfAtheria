using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalProjectile : GlobalProjectile
    {
        public static List<int> Eraser = new List<int>();

        public static List<int> ReflectAiList = new List<int>
        {
            1,
            2,
            3,
            8,
            18,
            16,
            21,
            23,
            24,
            25,
            27,
            28,
            29,
            32,
            33,
            34,
            36,
            40,
            41,
            43,
            44,
            45,
            46,
            47,
            48,
            49,
            50,
            51,
            55,
            56,
            57,
            58,
            60,
            65,
            68,
            70,
            71,
            72,
            74,
            75,
            80,
            81,
            82,
            83,
            88,
            91,
            92,
            93,
            95,
            96,
            102,
            105,
            106,
            107,
            108,
            109,
            112,
            113,
            115,
            116,
            118,
            119,
            126,
            129,
            131,
            143,
            144,
            147,
            151,
            179,
            181
        };

        public override void PostAI(Projectile projectile)
        {
            int type = projectile.type;
            if (Eraser.Contains(type))
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (ReflectAiList.Contains(proj.aiStyle))
                    {
                        if (projectile.Hitbox.Intersects(proj.Hitbox) && proj != projectile && proj.hostile)
                        {
                            proj.Kill();
                        }
                    }
                }
            }
        }
    }
}
