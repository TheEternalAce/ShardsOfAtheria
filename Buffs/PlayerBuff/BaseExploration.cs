using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class BaseExploration : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            Lighting.AddLight(player.Center, TorchID.White);
            player.nightVision = true;
            player.dangerSense = true;
            player.calmed = true;
        }
    }
}
