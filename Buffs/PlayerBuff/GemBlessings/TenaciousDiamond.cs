using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings
{
    public class TenaciousDiamond : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 4;
        }
    }
}
