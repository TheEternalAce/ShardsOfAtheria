using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class DiamondBarrierBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            int previousDefense = player.statDefense;
            int temporaryDefense = (int)(player.buffTime[buffIndex] * 0.05f);
            if (temporaryDefense > previousDefense)
            {
                player.buffTime[buffIndex] = (int)(previousDefense / 0.05f);
            }
            player.statDefense += temporaryDefense;
        }
    }
}
