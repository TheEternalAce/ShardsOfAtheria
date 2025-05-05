using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings
{
    public class DiamondBarrierBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            int buffTime = player.buffTime[buffIndex];
            if (player.HasBuff<EfficientAmethyst>()) player.buffTime[buffIndex]--;
            int maxDefense = 20;
            int divisor = 30;
            Math.Clamp(buffTime, 0, maxDefense * divisor);
            int temporaryDefense = buffTime / divisor;
            player.statDefense += Math.Max(2, temporaryDefense);
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return true;
        }
    }
}
