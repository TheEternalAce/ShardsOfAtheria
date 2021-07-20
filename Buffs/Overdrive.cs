using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class Overdrive : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Overdrive: ON");
            Description.SetDefault("'Your systems are being pushed beyond their limits'\n" +
                "Damage and movement speed increased by 50%\n" +
                "Defense lowered by 30\n" +
                "You cannot regen life");
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<SMPlayer>().overdriveTimeCurrent >= 0)
            {
                player.allDamage += .5f;
                player.statDefense -= 30;
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
