using ShardsOfAtheria.Utilities;
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
            if (player.Shards().overdriveTimeCurrent > 0)
            {
                player.GetDamage(DamageClass.Generic) += 1f;
                Lighting.AddLight(player.position, TorchID.Corrupt);
                player.buffTime[buffIndex] = 18000;
                player.armorEffectDrawOutlines = true;
                player.armorEffectDrawShadow = true;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
