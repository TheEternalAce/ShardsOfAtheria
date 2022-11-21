using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Items.SinfulSouls.Extras;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class SinfulPlayer : ModPlayer
    {
        public int SevenSoulUsed;
        public static readonly int[] SinfulBuffs = {
            ModContent.BuffType<EnvyBuff>(), ModContent.BuffType<GluttonyBuff>(), ModContent.BuffType<GreedBuff>(),
            ModContent.BuffType<LustBuff>(), ModContent.BuffType<PrideBuff>(), ModContent.BuffType<SlothBuff>(),
            ModContent.BuffType<WrathBuff>(), ModContent.BuffType<VirtuousSoul>()
        };

        public override void Initialize()
        {
            SevenSoulUsed = 0;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["SevenSoulUsed"] = SevenSoulUsed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("SevenSoulUsed"))
            {
                SevenSoulUsed = tag.GetInt("SevenSoulUsed");
            }
        }

        public override void PostUpdate()
        {
            if (SevenSoulUsed == 1)
            {
                Player.AddBuff(ModContent.BuffType<EnvyBuff>(), 2);
            }
            if (SevenSoulUsed == 2)
            {
                Player.AddBuff(ModContent.BuffType<GluttonyBuff>(), 2);
            }
            if (SevenSoulUsed == 3)
            {
                Player.AddBuff(ModContent.BuffType<GreedBuff>(), 2);
            }
            if (SevenSoulUsed == 4)
            {
                Player.AddBuff(ModContent.BuffType<LustBuff>(), 2);
            }
            if (SevenSoulUsed == 5)
            {
                Player.AddBuff(ModContent.BuffType<PrideBuff>(), 2);
            }
            if (SevenSoulUsed == 6)
            {
                Player.AddBuff(ModContent.BuffType<SlothBuff>(), 2);
            }
            if (SevenSoulUsed == 7)
            {
                Player.AddBuff(ModContent.BuffType<WrathBuff>(), 2);
            }
            if (SevenSoulUsed == 8)
            {
                Player.AddBuff(ModContent.BuffType<VirtuousSoul>(), 2);
            }
        }
    }
}
