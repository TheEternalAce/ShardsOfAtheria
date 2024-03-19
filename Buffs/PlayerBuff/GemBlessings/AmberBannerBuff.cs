using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings
{
    public class AmberBannerBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 20;
            player.moveSpeed += 0.2f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
        }
    }
}
