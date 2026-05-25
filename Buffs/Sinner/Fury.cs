using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Sinner
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
                Player.HurtInfo info = new()
                {
                    Damage = 200,
                    Dodgeable = false,
                    DamageSource = PlayerDeathReason.ByCustomReason(NetworkText.FromKey("Mods.ShardsOfAtheria.DeathMessages.WrathRage", player.name))
                };
                player.Hurt(info);
            }
            if (player.CardinalSoul().wrathRetainFuryTime > 0) player.buffTime[buffIndex]++;

            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.08f;
            player.moveSpeed += 0.1f;
        }
    }
}
