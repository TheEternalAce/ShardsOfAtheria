using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class Motivation : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.Sinner().sinID != SinnerPlayer.SLOTH) { player.DelBuff(buffIndex); buffIndex--; }
            player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
            player.moveSpeed += 0.1f;
            player.GetDamage(DamageClass.Generic) -= 0.15f;
        }
    }
}
