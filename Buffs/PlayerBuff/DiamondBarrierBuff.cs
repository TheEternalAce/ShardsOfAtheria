using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class DiamondBarrierBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            int maxDefense = 50;
            int temporaryDefense = (int)(player.buffTime[buffIndex] * 0.05f);
            if (temporaryDefense > maxDefense)
            {
                player.buffTime[buffIndex] = (int)(maxDefense / 0.05f);
            }
            player.statDefense += temporaryDefense;
        }
    }
}
