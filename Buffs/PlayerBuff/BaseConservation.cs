using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class BaseConservation : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.manaCost -= .1f;
            player.GetModPlayer<SoAPlayer>().baseConservation = true;
        }
    }
}
