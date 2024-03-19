using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings
{
    public class DiamondBarrierBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HasBuff<EfficientAmethyst>())
            {
                player.buffTime[buffIndex]--;
            }
            int maxDefense = 20;
            float multiplier = 0.05f;
            int temporaryDefense = (int)(player.buffTime[buffIndex] * multiplier);
            if (temporaryDefense > maxDefense)
            {
                player.buffTime[buffIndex] = (int)(maxDefense / multiplier);
            }
            player.statDefense += temporaryDefense;
        }
    }
}
