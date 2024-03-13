using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class BaseConservation : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.Slayer().omnicientTomePrevious)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            player.manaCost -= .1f;
            player.Shards().baseConservation = true;
        }
    }
}
