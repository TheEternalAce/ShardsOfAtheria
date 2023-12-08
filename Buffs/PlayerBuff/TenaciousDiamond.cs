using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class TenaciousDiamond : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 15;
        }
    }
}
