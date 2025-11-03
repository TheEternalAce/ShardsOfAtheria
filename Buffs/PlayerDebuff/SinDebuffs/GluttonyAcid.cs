using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerDebuff.SinDebuffs
{
    public class GluttonyAcid : ModBuff
    {
        public override void SetStaticDefaults()
        {
            BuffID.Sets.LongerExpertDebuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 20;
            player.GetDamage(DamageClass.Generic) -= 0.2f;
        }
    }
}
