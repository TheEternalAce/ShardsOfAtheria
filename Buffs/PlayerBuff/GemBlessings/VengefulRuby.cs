using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.GemBlessings
{
    public class VengefulRuby : ModBuff
    {
        public override void SetStaticDefaults()
        {
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.InCombat())
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }

            player.GetDamage(DamageClass.Generic) += 0.25f;
        }
    }
}
