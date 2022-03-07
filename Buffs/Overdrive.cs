using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class Overdrive : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Overdrive: ON");
            Description.SetDefault("'Your systems are being pushed beyond their limits'\n" +
                "Damage increased by 50%");
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<SoAPlayer>().overdriveTimeCurrent > 0)
            {
                player.GetDamage(DamageClass.Generic) += 1f;
                Lighting.AddLight(player.position, 0.5f, 0.5f, 0.5f);
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
