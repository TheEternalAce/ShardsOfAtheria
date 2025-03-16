using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings
{
    public class DiamondBarrierBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HasBuff<EfficientAmethyst>()) player.buffTime[buffIndex]--;
            int maxDefense = 20;
            float multiplier = 0.1f;
            int temporaryDefense = (int)(player.buffTime[buffIndex] * multiplier);
            Math.Clamp(player.buffTime[buffIndex], 0, (int)(maxDefense / multiplier));
            player.statDefense += Math.Max(1, temporaryDefense);
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return true;
        }
    }
}
