using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class Fury : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] == 0)
            {
                string gender = player.Male ? "his" : "her";
                PlayerDeathReason death = new()
                {
                    CustomReason = NetworkText.FromKey("ShardsOfAtheria.DeathMessages.WrathRage", player.name, gender)
                };
                player.KillMe(death, player.statLifeMax2 * 2, 1);
            }
            if (player.Sinner().retainFuryTime > 0) player.buffTime[buffIndex]++;

            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.08f;
            player.moveSpeed += 0.1f;
        }
    }
}
