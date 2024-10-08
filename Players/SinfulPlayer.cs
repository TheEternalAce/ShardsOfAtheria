﻿using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Items.SinfulSouls.Extras;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class SinfulPlayer : ModPlayer
    {
        public int SinfulSoulUsed;
        public static readonly int[] SinfulBuffs = {
            ModContent.BuffType<EnvyBuff>(), ModContent.BuffType<GluttonyBuff>(), ModContent.BuffType<GreedBuff>(),
            ModContent.BuffType<LustBuff>(), ModContent.BuffType<PrideBuff>(), ModContent.BuffType<SlothBuff>(),
            ModContent.BuffType<WrathBuff>(), ModContent.BuffType<VirtuousSoul>()
        };
        public const int Envy = 1;
        public const int Gluttony = 2;
        public const int Greed = 3;
        public const int Lust = 4;
        public const int Pride = 5;
        public const int Sloth = 6;
        public const int Wrath = 7;
        public const int Righteous = 8;

        public override void Initialize()
        {
            SinfulSoulUsed = 0;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["SevenSoulUsed"] = SinfulSoulUsed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("SevenSoulUsed"))
            {
                SinfulSoulUsed = tag.GetInt("SevenSoulUsed");
            }
        }

        public override void PostUpdate()
        {
            if (SinfulSoulUsed == ModContent.BuffType<EnvyBuff>())
            {
                Player.AddBuff(ModContent.BuffType<EnvyBuff>(), 2);
            }
            if (SinfulSoulUsed == ModContent.BuffType<GluttonyBuff>())
            {
                Player.AddBuff(ModContent.BuffType<GluttonyBuff>(), 2);
            }
            if (SinfulSoulUsed == ModContent.BuffType<GreedBuff>())
            {
                Player.AddBuff(ModContent.BuffType<GreedBuff>(), 2);
            }
            if (SinfulSoulUsed == ModContent.BuffType<LustBuff>())
            {
                Player.AddBuff(ModContent.BuffType<LustBuff>(), 2);
            }
            if (SinfulSoulUsed == ModContent.BuffType<PrideBuff>())
            {
                Player.AddBuff(ModContent.BuffType<PrideBuff>(), 2);
            }
            if (SinfulSoulUsed == ModContent.BuffType<SlothBuff>())
            {
                Player.AddBuff(ModContent.BuffType<SlothBuff>(), 2);
            }
            if (SinfulSoulUsed == ModContent.BuffType<WrathBuff>())
            {
                Player.AddBuff(ModContent.BuffType<WrathBuff>(), 2);
            }
            if (SinfulSoulUsed == ModContent.BuffType<VirtuousSoul>())
            {
                Player.AddBuff(ModContent.BuffType<VirtuousSoul>(), 2);
            }
        }
    }
}
