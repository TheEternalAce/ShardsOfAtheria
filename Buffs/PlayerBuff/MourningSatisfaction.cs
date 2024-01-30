using ShardsOfAtheria.Buffs.PlayerDebuff;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class MourningSatisfaction : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.buffImmune[ModContent.BuffType<CorruptedBlood>()] = true;
            player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
        }
    }
}
