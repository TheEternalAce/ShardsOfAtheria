using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class Overdrive : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<SoAPlayer>().overdriveTimeCurrent > 0)
            {
                player.GetDamage(DamageClass.Generic) += 1f;
                Lighting.AddLight(player.position, TorchID.Corrupt);
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
