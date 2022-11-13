using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class BaseCombat : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 10;
            player.GetDamage(DamageClass.Generic) += .1f;
            player.thorns = 1;
        }
    }
}
